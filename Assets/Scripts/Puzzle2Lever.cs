using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Lever : MonoBehaviour
{
    [SerializeField] private int leverNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerInteracts()
    {
        Puzzle2 puzzleScript = this.transform.parent.GetComponent<Puzzle2>();
        puzzleScript.ActivateLever(1);
    }
}
