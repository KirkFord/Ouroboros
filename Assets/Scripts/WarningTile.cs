using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTile : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    private int delay = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        delay += 1;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if(delay > 70)
            {
                GameObject fx = Instantiate(effect, transform.position, transform.rotation);
                Destroy(fx, 1.5f);
                delay = 0;
            }
        }
            
    }
}
