using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private Player player;
    private float healAmt = 5.0f;
    private float healInterval = 1.0f; // in seconds
    private float lastHealTime = 0.0f;
    private float healRange = 4.0f;
    
    private void Update()
    {
        Heal();
    }

    private void Heal() {
        if (Vector3.Distance(transform.position, player.transform.position) < healRange && Time.time > lastHealTime + healInterval) {
            player.Heal(healAmt);
            lastHealTime = Time.time;
        }
    }
}
