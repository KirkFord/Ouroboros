using UnityEngine;

public class DoorInteractZone : MonoBehaviour
{
   [SerializeField] private Door myDoor;

   private void OnTriggerEnter(Collider other)
   {
      if (!other.CompareTag("Player")) return;
      myDoor.PlayerEnteredZone();
   }
   private void OnTriggerExit(Collider other)
   {
      if (!other.CompareTag("Player")) return;
      myDoor.PlayerLeftZone();
   }
}
