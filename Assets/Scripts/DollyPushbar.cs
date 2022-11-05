using UnityEngine;

public class DollyPushbar : MonoBehaviour
{
    private CameraDolly _cD;

    private void Start()
    {
        _cD = transform.parent.GetComponent<CameraDolly>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cD.CheckMovement();  
        }
    }
}
