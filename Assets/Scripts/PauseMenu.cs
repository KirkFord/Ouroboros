using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private BGM _bgm;
    private static bool _gameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) || SceneManager.GetActiveScene().name.Equals("StartScreen")) return;
        if (_gameIsPaused){
            Resume();
        }
        else {
            Pause();
        }

    }

    public void Start()
    {
        _bgm = BGM.Instance;
        
    }

    public void Resume () {
        _bgm.PlaySound(Sound.PauseMenuFX);
        _bgm.musicAudioSource.UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;

    }

    private void Pause () {
        _bgm.PlaySound(Sound.PauseMenuFX);
        _bgm.musicAudioSource.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
        
    }

    public void LoadMenu(){
        Debug.Log("PRESSING A FUCKING BUTTON");
        _bgm.PlaySound(Sound.MenuSelectFX);
        _gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.ShutPlayerUp();
        SceneManager.LoadScene("StartScreen");
        
    }

    public void QuitGame(){
        _bgm.PlaySound(Sound.MenuSelectFX);
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
}
