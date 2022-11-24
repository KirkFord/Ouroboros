using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private BGM bgm;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !SceneManager.GetActiveScene().name.Equals("StartScreen"))
        {
            if (GameIsPaused){
                Resume();
            }
            else {
                Pause();
            }
            
        }
        
    }

    public void Start()
    {
        bgm = BGM.instance;
        
    }

    public void Resume () {
        bgm.PlaySound(BGM.Sound.PauseMenuFX);
        bgm.MusicAudioSource.UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause () {
        bgm.PlaySound(BGM.Sound.PauseMenuFX);
        bgm.MusicAudioSource.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
    }

    public void LoadMenu(){
        Debug.Log("PRESSING A FUCKING BUTTON");
        bgm.PlaySound(BGM.Sound.MenuSelectFX);
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.ShutPlayerUp();
        SceneManager.LoadScene("StartScreen");
        
    }

    public void QuitGame(){
        bgm.PlaySound(BGM.Sound.MenuSelectFX);
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
}
