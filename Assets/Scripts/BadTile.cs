using UnityEngine;

public class BadTile : MonoBehaviour
{
    private int breakTimer = 15;
    private GameObject player;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject beginning;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (breakTimer <= 0)
        {
            BrokeTile();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            breakTimer -= 1;
        }
    }

    void BrokeTile()
    {
        var explosionObj = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionObj, 1.5f);
        Instantiate(blocker, transform.position, transform.rotation);
        GotoBeginning();
        Destroy(gameObject);
    }

    void GotoBeginning()
    {
        player.transform.position = beginning.transform.position;
        player.GetComponent<Player>().PlayerTakeDamage(5.0f, true);
    }
}
