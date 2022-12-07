using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private float healAmt = 50.0f;
    private bool _playerInArea;
    private bool _healUsed;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || _healUsed) return;
        
        InteractionManager.Instance.ShowInteractText($"Press [F] to heal {healAmt} hp");
        _playerInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || _healUsed) return;
        
        InteractionManager.Instance.HideInteractText();
        _playerInArea = false;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F) || !_playerInArea) return;
        
        Player.Instance.Heal(healAmt);
        _healUsed = true;
        InteractionManager.Instance.HideInteractText();
    }
}
