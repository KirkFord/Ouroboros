using UnityEngine;

public class Drop : MonoBehaviour
{
    private Player _player;
    private float moveSpeed;

    private void Start() {
        _player = Player.Instance;
    }

    private void Update() {
        //Magnetize();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        Debug.Log("drop hit by player");
        Destroy(gameObject);
    }


    // private void Magnetize()
    // {
    //     if (!(Vector3.Distance(this.transform.position, _player.transform.position) < 4.0f)) return;
    //     moveSpeed += 0.001f; // make coin accelerate
    //     transform.LookAt(_player.transform.position);
    //     transform.position += transform.forward * moveSpeed * Time.deltaTime;
    // }
}
