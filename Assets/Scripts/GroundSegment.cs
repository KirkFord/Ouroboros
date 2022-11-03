using System.Collections;
using UnityEngine;

public class GroundSegment : MonoBehaviour
{
    private GroundController _gc;
    private bool _moving = true;
    
    private void Start()
    {
        _gc = transform.parent.GetComponent<GroundController>();
        _gc.AllEnemiesKilled += StopMoving;
        
        StartCoroutine(MoveSegment());
    }

    public void Despawn()
    {
        _gc.AllEnemiesKilled -= StopMoving;
        Destroy(gameObject);
    }

    private IEnumerator MoveSegment()
    {
        while (_moving)
        {
            transform.Translate(new Vector3(0,0, -_gc.cameraPanSpeed * Time.deltaTime));
            yield return null;
        }
    }

    private void StopMoving()
    {
        _moving = false;
    }
}
