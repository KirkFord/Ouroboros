using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeField] private GameObject TPPlayerOnLoad;
    [SerializeField] private GameObject Start1;
    [SerializeField] private GameObject Start2;
    [SerializeField] private GameObject Start3;
    [SerializeField] private GameObject Camera1;
    [SerializeField] private GameObject Camera2;
    [SerializeField] private GameObject Camera3;
    private GameObject cameraChosen;
    private GameObject ChosenLocation = null;
    private bool cameraSnapped = true;
    // Start is called before the first frame update
    void Start()
    {
        ChooseVariation();
        SnapCamera();
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

    void ChooseVariation()
    {
        int chosen = Random.Range(1, 3);
        
        switch (chosen) 
        {
            case 1:
                Debug.Log("Chose 1");
                ChosenLocation = Start1;
                cameraChosen = Camera1;
                break;
            case 2:
                Debug.Log("Chose 2");
                ChosenLocation = Start2;
                cameraChosen = Camera2;
                break;
            case 3:
                Debug.Log("Chose 3");
                ChosenLocation = Start3;
                cameraChosen = Camera3;
                break;
        }
        Instantiate(TPPlayerOnLoad, ChosenLocation.transform.position, ChosenLocation.transform.rotation);
    }

    void SnapCamera()
    {
        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        camObj.transform.position = cameraChosen.transform.position;
        camObj.transform.rotation = cameraChosen.transform.rotation;
    }
}
