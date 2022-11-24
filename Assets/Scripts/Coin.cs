using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   private CoinManager _cm;
   private Player _player;
   private float moveSpeed = 0.0f;
   
   void Start() {
    _cm = CoinManager.Instance;
    _player = Player.Instance;
   }

    void Update() {
        moveTowardsPlayer();
    }
   
   void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            Debug.Log("coin hit by player");
            _cm.AddCoins(1);
            Destroy(this.gameObject);
        }
    }

    
    void moveTowardsPlayer() {
        if (Vector3.Distance(this.transform.position, _player.transform.position) < 4.0f) {
            moveSpeed += 0.008f; // make coin accelerate
            transform.LookAt(_player.transform.position);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
