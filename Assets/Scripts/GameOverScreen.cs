using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    //public static bool GamePaused = false;
    public GameObject GameOverUI;

    public static GameOverScreen Instance;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
    }
    public void dead()
    {
        GameOverUI.SetActive(true);
        // Time.timeScale = 0f;
    }
}
