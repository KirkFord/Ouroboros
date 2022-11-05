using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour
{
    //private Transform target;
    public bool InMagnet = false;
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Pickup"))
        {
            // var step = speed * Time.deltaTime;
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position,speed*Time.deltaTime);
            //other.transform.LookAt(GameManager.instance.player.transform.position);
            //other.transform.position += other.transform.forward * (speed * Time.deltaTime);
        }
    }
    
}
