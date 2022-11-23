using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume () {
        BGM.instance.PlaySound(BGM.Sound.PauseMenuFX);
        BGM.instance.MusicAudioSource.UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause () {
        BGM.instance.PlaySound(BGM.Sound.PauseMenuFX);
        BGM.instance.MusicAudioSource.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu(){
        BGM.instance.PlaySound(BGM.Sound.MenuSelectFX);
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }

    public void QuitGame(){
        BGM.instance.PlaySound(BGM.Sound.MenuSelectFX);
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
