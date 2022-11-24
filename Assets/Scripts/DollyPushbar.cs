using UnityEngine;

public class DollyPushbar : MonoBehaviour
{
    private CameraDolly _cD;
    private GameManager _gM;
    private bool _followingPlayer;

    private void Start()
    {
        _gM = GameManager.Instance;
        _cD = transform.parent.GetComponent<CameraDolly>();
        _followingPlayer = _cD.followingPlayer;
        _gM.AllEnemiesKilled += ToggleFollowPlayer;
    }
    private void OnDestroy()
    {
        _gM.AllEnemiesKilled -= ToggleFollowPlayer;
    }

    private void ToggleFollowPlayer()
    {
        _followingPlayer = !_followingPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _followingPlayer) return;
        _gM.TerrainSpeedIncrease();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || !_followingPlayer) return;
        Debug.Log("Player Hitting Pushbar");
        _cD.CheckMovement();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gM.TerrainSpeedDecrease();
        }
    }
}
