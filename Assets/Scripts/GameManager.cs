using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GroundController _gc;
    private EnemiesManager _eM;
    public event Action AllEnemiesKilled;
    private int _enemiesRemaining;
    public float terrainMoveSpeed = 3.0f;

    private int _loops;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded " + scene.name);
        
        switch (scene.name)
        {
            case "MainHall":
                LoadMainHall();
                break;
            
            case "HealingRoom":
                break;
        }
    }

    private void LoadMainHall()
    {
        if (_eM == null)
        {
            _eM = EnemiesManager.instance;
            _eM.EnemyKilled += EnemyDied;
            _eM.SetUpNextLevel(_enemiesRemaining); 
        } 
        
        _loops += 1;
        _enemiesRemaining = 10 * _loops;
        _eM.SetUpNextLevel(_enemiesRemaining); 
        StartCoroutine(CheckEnemiesRemaining());
    }

    private void EnemyDied()
    {
        _enemiesRemaining -= 1;
    }

    private IEnumerator CheckEnemiesRemaining()
    {
        while (_enemiesRemaining > 0) yield return null;
        AllEnemiesKilled?.Invoke();
    }
}
