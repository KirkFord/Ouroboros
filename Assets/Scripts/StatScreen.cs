using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatScreen : MonoBehaviour
{
    public GameObject StatUI;
    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //     }
    //     else if (Instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    //     DontDestroyOnLoad(gameObject);
    // }
    public void showStats()
    {
        StatUI.SetActive(true);
        // Time.timeScale = 0f;
        
    }
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
