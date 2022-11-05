using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GroundSegment : MonoBehaviour
{
    private GameManager _gm;
    private bool _moving = true;
    
    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (SceneManager.GetActiveScene().name != "HealingRoom") {
            _gm.AllEnemiesKilled += StopMoving;
        
            StartCoroutine(MoveSegment());
        }
    }

    public void Despawn()
    {
        _gm.AllEnemiesKilled -= StopMoving;
        Destroy(gameObject);
    }

    private IEnumerator MoveSegment()
    {
        while (_moving)
        {
            transform.Translate(new Vector3(0,0, -_gm.terrainMoveSpeed * Time.deltaTime));
            yield return null;
        }
    }

    private void StopMoving()
    {
        _moving = false;
    }
}
