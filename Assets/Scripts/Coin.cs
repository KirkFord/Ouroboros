using UnityEngine;

public class Coin : MonoBehaviour
{
   private CoinManager _cm;
   private Player _player;
   private float moveSpeed;
   private Rigidbody _rb;
   
   private void Start() {
    _cm = CoinManager.Instance;
    _player = Player.Instance;
    _rb = GetComponent<Rigidbody>();
   }

   private void Update() {
        Magnetize();
        _rb.velocity = !GameManager.Instance.walkingToEnd ? new Vector3(0, 0, -GameManager.Instance.terrainMoveSpeed) : new Vector3(0, 0, 0);
    }

   private void OnTriggerEnter(Collider other)
   {
       if (!other.transform.CompareTag("Player")) return;
       AddCoin();

   }
   private void AddCoin()
   {
       _cm.AddCoins(1);
       Destroy(gameObject);
   }

   private void Magnetize()
   {
       if (!(Vector3.Distance(this.transform.position, _player.transform.position) < 4.0f)) return;
       moveSpeed += 0.001f; // make coin accelerate
       transform.LookAt(_player.transform.position);
       transform.position += transform.forward * moveSpeed * Time.deltaTime;
   }
}
