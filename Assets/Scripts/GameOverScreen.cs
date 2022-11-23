using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    //public BGM bgm;
    public StatScreen stats;
    //public static bool GamePaused = false;
    public GameObject GameOverUI;
    
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
    public void dead()
    {
        GameOverUI.SetActive(true);
        // Time.timeScale = 0f;
    }
    public void LoadStats(){
        BGM.instance.PlaySound(BGM.Sound.MenuSelectFX);
        GameOverUI.SetActive(false);
        BGM.instance.StatsSwitch();
        stats.showStats();
        //Time.timeScale = 1f;
    }
}
