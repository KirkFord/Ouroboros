using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Drop : MonoBehaviour
{
    [SerializeField] private PickupType type;
    
    private delegate void OnPickup();

    private OnPickup _onPickup;
    private List<PickupType> randomPickups;
    private bool pickedUp;
    
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
            PickupType.XP => XpPickUp,
            _ => _onPickup

        };
    }

    private void OnDestroy()
    {
        if (!pickedUp) return;
        _onPickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
            pickedUp = true;
        }
        if (other.transform.CompareTag("EnemyOverflow"))
        {
            Destroy(gameObject);
        }
        
    }
    private void HealPickUp()
    {
        Debug.Log("Heal Pickup Grabbed");
        Player.Instance.Heal(20.0f);
        DamagePopup.CreatePlayerPopup(Player.Instance.transform.position, "+20 HP");
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
        Player.Instance.ActivateInvincibility();
    }
    private void AttackSpeedPickUp()
    {
        Debug.Log("Attack Speed Pickup Grabbed");
        Player.Instance.ActivateAttackSpeed();
    }
    private void CoinMultiplierPickUp()
    {
        Debug.Log("Coin Multiplier Pickup Grabbed");
        Player.Instance.ActivateCoinMultiplier();
    }
    private void XpPickUp()
    {
        Player.Instance.gainXP(1);
        Debug.Log("Coin Multiplier Pickup Grabbed");
    }
}
