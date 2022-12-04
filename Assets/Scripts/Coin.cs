using UnityEngine;

public class Coin : MonoBehaviour
{
   private CoinManager _cm;
   private Player _player;
   private float moveSpeed;

   private void Start() {
    _cm = CoinManager.Instance;
    _player = Player.Instance;
   }

   private void Update() {
        Magnetize();
    }

   private void OnTriggerEnter(Collider other)
   {
       if (!other.transform.CompareTag("Player")) return;
       Debug.Log("coin hit by player");
       _cm.AddCoins(1);
       Destroy(gameObject);
   }


   private void Magnetize()
   {
       if (!(Vector3.Distance(this.transform.position, _player.transform.position) < 4.0f)) return;
       moveSpeed += 0.004f; // make coin accelerate
       transform.LookAt(_player.transform.position);
       transform.position += transform.forward * moveSpeed * Time.deltaTime;
   }
}
