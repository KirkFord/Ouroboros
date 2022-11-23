using UnityEngine;

public class ChestHealButton : MonoBehaviour
{
    
   private CampFire campfire;

   private void ChestHeal() {
    campfire.Heal();
   }
}
