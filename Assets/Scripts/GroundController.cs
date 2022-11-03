using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundController : MonoBehaviour
{
    [SerializeField] private List<GameObject> segments;
    private Queue<GameObject> _currentSegments;
    private GameObject _lastSpawnedSegment;
    [SerializeField] private GameObject endDoorways;

    [SerializeField] private List<GameObject> doorList;

    private GameObject _start;
    [SerializeField] private GameObject end;
    [SerializeField] private CameraDolly cDolly;
    private GameManager _gm;
    private bool _levelComplete;
    
    

    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gm.AllEnemiesKilled += LevelComplete;
        _currentSegments = new Queue<GameObject>();
        _start = GameObject.Find("Start");
        AddSegment(_start);
        _lastSpawnedSegment = _start;
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (!_levelComplete)
        {
            var segToSpawn = segments[Random.Range(0, segments.Count)];
            var segSize = segToSpawn.transform.localScale.z;
            var spawnSpot = new Vector3(0, 0, _lastSpawnedSegment.transform.position.z + _lastSpawnedSegment.transform.localScale.z/2 + segSize/2);

            var segment = Instantiate(segToSpawn, spawnSpot, segToSpawn.transform.rotation);
            segment.transform.SetParent(transform);
            _lastSpawnedSegment = segment;
            AddSegment(segment);
            yield return new WaitForSeconds(segSize/_gm.terrainMoveSpeed);  
        }
    }

    private void AddSegment(GameObject seg)
    {
        _currentSegments.Enqueue(seg);
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
            RemoveSegment().GetComponent<GroundSegment>().Despawn();
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
        var lenOfDoorway = endDoorways.transform.localScale.z;
        var endSpot = new Vector3(0, 0, _lastSpawnedSegment.transform.position.z + segmentLength/2 + lenOfDoorway/2);
        end.transform.position = endSpot;
        end.gameObject.SetActive(true);

        var leftDoorSlot = end.transform.GetChild(0);
        var rightDoorSlot = end.transform.GetChild(1);

        var leftDoor = doorList[Random.Range(0, doorList.Count)];
        doorList.Remove(leftDoor);
        var rightDoor = doorList[Random.Range(0, doorList.Count)];
        Instantiate(leftDoor, leftDoorSlot.transform.position, leftDoor.transform.rotation);
        Instantiate(rightDoor, rightDoorSlot.transform.position, rightDoor.transform.rotation);
    }
}
