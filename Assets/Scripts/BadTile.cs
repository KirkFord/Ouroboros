using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTile : MonoBehaviour
{
    private int breakTimer = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (breakTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            breakTimer -= 1;
        }
    }

    //Get parent object
    //When player stands on tile start timer
    //When timer runs out, player dies?
}
