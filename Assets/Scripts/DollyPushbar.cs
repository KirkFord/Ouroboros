using UnityEngine;

public class DollyPushbar : MonoBehaviour
{
    private CameraDolly _cD;
    private GameManager _gM;

    private void Start()
    {
        _gM = GameManager.Instance;
        _cD = transform.parent.GetComponent<CameraDolly>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _cD.followingPlayer) return;
        _gM.TerrainSpeedIncrease();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name != "Player" || !_cD.followingPlayer) return;
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
