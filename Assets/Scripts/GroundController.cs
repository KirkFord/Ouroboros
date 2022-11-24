using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GroundController : MonoBehaviour
{
    [SerializeField] private List<GameObject> segments;
    private Queue<GameObject> _currentSegments;
    [SerializeField] private GameObject _lastSpawnedSegment;

    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject end;

    [SerializeField] private Door leftDoor;
    [SerializeField] private Door rightDoor;

    [SerializeField] private Sprite shopIcon;
    [SerializeField] private Sprite puzzleIcon;
    [SerializeField] private Sprite healIcon;

    [SerializeField] private Image leftDoorImage;
    [SerializeField] private Image rightDoorImage;
    
    [SerializeField] private CameraDolly cDolly;
    private GameManager _gm;
    [SerializeField]private bool _levelComplete;

    private void Start()
    {
        _gm = GameManager.Instance;
        
        _currentSegments = new Queue<GameObject>();
        _gm.AllEnemiesKilled += LevelComplete;
        AddSegment(_start);
        _lastSpawnedSegment = _start;
    }

    private void OnDestroy()
    {
        _gm.AllEnemiesKilled -= LevelComplete;
    }

    public void SpawnSegment()
    {
        var segToSpawn = segments[Random.Range(0, segments.Count)];
        var segSize = segToSpawn.transform.localScale.z;
        var spawnSpot = new Vector3(0, 0, _lastSpawnedSegment.transform.position.z + _lastSpawnedSegment.transform.localScale.z/2 + segSize/2);
        var segment = Instantiate(segToSpawn, spawnSpot, segToSpawn.transform.rotation);
        segment.transform.SetParent(transform);
        AddSegment(segment);
    }

    private void AddSegment(GameObject seg)
    {
        _currentSegments.Enqueue(seg);
        _lastSpawnedSegment = seg;
        CheckQueueSize();
    }

    private GameObject RemoveSegment()
    {
        return _currentSegments.Dequeue();
    }

    private void CheckQueueSize()
    {
        //MAX SEGMENTS ALLOWED
        if (_currentSegments.Count > 8)
        {
            Destroy(RemoveSegment());
        }
    }
    

    private void LevelComplete()
    {
        _levelComplete = true;
        SummonEndPlatform();
        cDolly.FollowPlayer();
    }

    private  void SummonEndPlatform()
    {
        var segmentLength = _lastSpawnedSegment.transform.localScale.z;
        var lenOfDoorway = end.transform.localScale.z;
        var endSpot = new Vector3(0, 0, _lastSpawnedSegment.transform.position.z + segmentLength/2 + lenOfDoorway/2);
        end.transform.position = endSpot;
        //var endPlatform = Instantiate(end, endSpot, end.transform.rotation);
        cDolly.SetEnd(end);

        var possibleLevels = new List<Level>
        {
            Level.HealLevel,
            Level.PuzzleLevel1,
            Level.PuzzleLevel2,
            Level.ShopLevel
        };

        var leftSelection = possibleLevels[Random.Range(0,possibleLevels.Count)];
        switch (leftSelection)
        {
            case Level.PuzzleLevel2:
                possibleLevels.Remove(Level.PuzzleLevel1);
                break;
            case Level.PuzzleLevel1:
                possibleLevels.Remove(Level.PuzzleLevel2);
                break;
        }

        possibleLevels.Remove(leftSelection);
        var rightSelection = possibleLevels[Random.Range(0,possibleLevels.Count)];
        Debug.Log("Left Selection: "+ leftSelection);
        Debug.Log("Right Selection: "+ rightSelection);
        
        switch (leftSelection)
        {
            case Level.HealLevel:
                leftDoorImage.sprite = healIcon;
                break;
            case Level.PuzzleLevel1:
                leftDoorImage.sprite = puzzleIcon;
                break;
            case Level.PuzzleLevel2:
                leftDoorImage.sprite = puzzleIcon;
                break;
            case Level.ShopLevel:
                leftDoorImage.sprite = shopIcon;
                break;
        }
        
        switch (rightSelection)
        {
            case Level.HealLevel:
                rightDoorImage.sprite = healIcon;
                break;
            case Level.PuzzleLevel1:
                rightDoorImage.sprite = puzzleIcon;
                break;
            case Level.PuzzleLevel2:
                rightDoorImage.sprite = puzzleIcon;
                break;
            case Level.ShopLevel:
                rightDoorImage.sprite = shopIcon;
                break;
        }
        
        
        leftDoor.SetDoorPath(leftSelection);
        rightDoor.SetDoorPath(rightSelection);
    }
}
