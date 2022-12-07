using UnityEngine;

public class ShopKeeperZone : MonoBehaviour
{
    [SerializeField] private ShopSystem ss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ss.EnteredZone();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ss.LeftZone();
    }
}
