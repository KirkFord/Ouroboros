using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GroundController : MonoBehaviour
{
    [SerializeField] private CameraDolly cDolly;
    
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject end;
    [SerializeField] private Door leftDoor;
    [SerializeField] private Image leftDoorImage;
    [SerializeField] private Door rightDoor;
    [SerializeField] private Image rightDoorImage;
    [SerializeField] private Sprite shopIcon;
    [SerializeField] private Sprite puzzleIcon;
    [SerializeField] private Sprite healIcon;

    [SerializeField] private List<GameObject> segments;
    
    private GameObject _lastSpawnedSegment;
    

    private void Start()
    {
        GameManager.Instance.AllEnemiesKilled += LevelComplete;
        _lastSpawnedSegment = _start;
    }

    private void OnDestroy()
    {
        GameManager.Instance.AllEnemiesKilled -= LevelComplete;
    }

    public void SpawnSegment()
    {
        var segToSpawn = segments[Random.Range(0, segments.Count)];
        var segSize = segToSpawn.transform.localScale.z;
        var spawnSpot = new Vector3(0, 0, _lastSpawnedSegment.transform.position.z + _lastSpawnedSegment.transform.localScale.z/2 + segSize/2);
        var segment = Instantiate(segToSpawn, spawnSpot, segToSpawn.transform.rotation);
        segment.transform.SetParent(transform);
        _lastSpawnedSegment = segment;
    }

    private void LevelComplete()
    {
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

        var images = new Dictionary<Level, Sprite>
        {
            {Level.HealLevel, healIcon},
            {Level.PuzzleLevel1, puzzleIcon},
            {Level.PuzzleLevel2, puzzleIcon},
            {Level.ShopLevel, shopIcon}
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
        rightDoorImage.sprite = images[rightSelection];
        leftDoorImage.sprite = images[leftSelection];
        leftDoor.SetDoorPath(leftSelection);
        rightDoor.SetDoorPath(rightSelection);
    }
}
