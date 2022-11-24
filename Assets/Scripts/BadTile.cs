using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTile : MonoBehaviour
{
    private int breakTimer = 30;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject blocker;
    [SerializeField] private Vector3 gotoBeginning;
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
        player.transform.position = gotoBeginning;
        Instantiate(blocker, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
