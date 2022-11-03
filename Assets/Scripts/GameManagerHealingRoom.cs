using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerHealingRoom : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject leftFrame;
    [SerializeField] private GameObject rightFrame;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switchToMainScene();
    }

    private void switchToMainScene() {        
        if (isInDoorFrame(player)) {
            Debug.Log("SceneManager.GetSceneByName(MainHall): " + SceneManager.GetSceneByName("MainHall").name);
            SceneManager.LoadScene("MainHall");
        }
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
