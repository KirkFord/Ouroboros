using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private PickupType type;
    
    private delegate void OnPickup();

    private OnPickup _onPickup;
    private List<PickupType> randomPickups;
    
    private void Start()
    {
        randomPickups = new List<PickupType>
        {
            PickupType.Heal,
            PickupType.Invinciblility,
            PickupType.AttackSpeed,
            PickupType.CoinMultiplier
        };
        SetPickupFunction(type);
    }

    private void SetPickupFunction(PickupType pickupType)
    {
        _onPickup = pickupType switch
        {
            PickupType.Heal => HealPickUp,
            PickupType.Invinciblility => InvincibilityPickUp,
            PickupType.Random => RandomPickUp,
            PickupType.AttackSpeed => AttackSpeedPickUp,
            PickupType.CoinMultiplier => CoinMultiplierPickUp,
            PickupType.XP => XPPickUp,
            _ => _onPickup

        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("drop hit by player");
            _onPickup();
            Destroy(gameObject);
            return;
        }
        if (other.transform.CompareTag("EnemyOverflow"))
        {
            Destroy(gameObject);
        }
        
    }
    private static void HealPickUp()
    {
        Debug.Log("Heal Pickup Grabbed");
        Player.Instance.Heal(20.0f);
    }
    private void RandomPickUp()
    {
        Debug.Log("Random Pickup Grabbed");
        var choice = randomPickups[Random.Range(0, randomPickups.Count)];
        SetPickupFunction(choice);
        _onPickup();
    }
    private void InvincibilityPickUp()
    {
        Debug.Log("Invincibility Pickup Grabbed");
    }
    private void AttackSpeedPickUp()
    {
        Debug.Log("Attack Speed Pickup Grabbed");
    }
    private void CoinMultiplierPickUp()
    {
        Debug.Log("Coin Multiplier Pickup Grabbed");
    }
    private void XPPickUp()
    {
        Player.Instance.gainXP(1);
        Debug.Log("Coin Multiplier Pickup Grabbed");
    }
}
