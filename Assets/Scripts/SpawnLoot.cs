using UnityEngine;

public class SpawnLoot : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    public void DropCoin()
    {
        Instantiate(coin, transform.position, transform.rotation);
    }
}
