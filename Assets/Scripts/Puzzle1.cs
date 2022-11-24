using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{

    private bool cameraSnapped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!cameraSnapped)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
                camObj.transform.SetParent(playerObj.transform);
                cameraSnapped = true;
            }
        }
    }
}
