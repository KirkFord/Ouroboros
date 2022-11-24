using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Lever : MonoBehaviour
{
    [SerializeField] private int leverNum = 0;
    private bool playerIn = false;
    private bool textShowing = false;
    private InteractionManager _iM;
    // Start is called before the first frame update
    void Start()
    {
        _iM = InteractionManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Lever Pressed!");
                PlayerInteracts();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _iM.ShowInteractText("Press [F] to Interact");
            playerIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _iM.HideInteractText();
            playerIn = false;
        }
    }

    void PlayerInteracts()
    {
        Puzzle2 puzzleScript = this.transform.parent.GetComponent<Puzzle2>();
        puzzleScript.ActivateLever(leverNum);
    }

}
