using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerHealingRoom : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject leftFrame;
    [SerializeField] private GameObject rightFrame;
    private int i = 0;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if (isInDoorFrame(player) && i == 0) {
            i = 1;
            setMainScene();
        } else if (i == 1) {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainHall"));        
        }
    }

    private void setMainScene() {
        SceneManager.LoadScene("MainHall");
    }

    private bool isInDoorFrame(GameObject go) {
        if ((go.transform.position.x - go.transform.localScale.x / 2 > 
            leftFrame.transform.position.x + leftFrame.transform.localScale.x / 2) &&
            (go.transform.position.x + go.transform.localScale.x / 2 < 
            rightFrame.transform.position.x - rightFrame.transform.localScale.x / 2) &&
            (go.transform.position.z <= leftFrame.transform.position.z)) {
            return true;
        }
        return false;
    }
}
