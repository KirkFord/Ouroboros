using UnityEngine;


public class GroundSegment : MonoBehaviour
{
    private GameManager _gM;
    private GroundController _gC;
    private bool _moving = true;

    private void Start()
    {
        _gM = GameManager.Instance;
        _gC = GameObject.Find("GroundController").GetComponent<GroundController>();
        _gM.AllEnemiesKilled += StopMoving;
    }
    public void OnDestroy()
    {
        _gM.AllEnemiesKilled -= StopMoving;
    }

    private void Update()
    {
        if (!_moving || _gM.walkingToEnd) return;
        transform.Translate(new Vector3(0,0, -_gM.terrainMoveSpeed * Time.deltaTime));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DeleteSegment"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("GroundSegmentSpawningZone"))
        {
            _gC.SpawnSegment(); 
        }
    }
    private void StopMoving()
    {
        _moving = false;
    }
}
