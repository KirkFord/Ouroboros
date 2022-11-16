using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Magic_Shoot : MonoBehaviour
{
    private GameObject target;
    private bool hasBeenMade = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void Created(GameObject passTarget)
    {
        target = passTarget;
        hasBeenMade = true;
    }
}
