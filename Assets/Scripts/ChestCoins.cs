using UnityEngine;

public class ChestCoins : MonoBehaviour
{
    private bool playerTouching;
    private bool hasOpened;
    [SerializeField] private Puzzle2 puz2;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hasOpened)
            {
                InteractionManager.Instance.HideInteractText();
                return;
            }
            InteractionManager.Instance.ShowInteractText("Press [F] to open Chest");
            playerTouching = true;  
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTouching = false;
            InteractionManager.Instance.HideInteractText();
        }
    }

    private void Update()
    {
        if (playerTouching && Input.GetKeyDown(KeyCode.F))
        {
            if (hasOpened) return;
            hasOpened = true;
            puz2.GiveReward();
        }
    }
}
