using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundController : MonoBehaviour
{
    [SerializeField] private List<GameObject> segments;
    [SerializeField] private List<GameObject> currentSegments;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float cameraPanSpeed = 3.0f;
    private bool _spawning = true;

    private void Start()
    {
        for(var i = 0; i <transform.childCount-1; i++)
        {
            var child = transform.GetChild(i);
            if (child.gameObject.layer != 6) continue;
            currentSegments.Add(child.gameObject);
        }
        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        MoveChildren();
    }

    private IEnumerator SpawnLoop()
    {
        while (_spawning)
        {
            var segToSpawn = segments[Random.Range(0, segments.Count-1)];
            var segment = Instantiate(segToSpawn, spawnLocation.transform.position, segToSpawn.transform.rotation);
            AddSegment(segment);
            yield return new WaitForSeconds(7.5f);  
        }
    }

    private void MoveChildren()
    {
        foreach (var child in currentSegments)
        {
            child.transform.Translate(new Vector3(0,0, -cameraPanSpeed * Time.deltaTime));
        }
    }
    
    private void AddSegment(GameObject seg)
    {
        currentSegments.Add(seg);
    }

    public void RemoveSegment(GameObject seg)
    {
        currentSegments.Remove(seg);
    }
}
