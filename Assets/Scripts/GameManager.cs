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
    [SerializeField]private int enemiesRemaining = 10;
    public float terrainMoveSpeed = 3.0f;

    private int loops = 0;
    private void Awake()
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

    private void Start()
    {
        _eM = GameObject.Find("EnemiesManager").GetComponent<EnemiesManager>().instance;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _eM.EnemyKilled += EnemyDied;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        _eM.EnemyKilled -= EnemyDied;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
        loops += 1;
        _eM.EnemiesSpawned = 10 * loops;
        StartCoroutine(CheckEnemiesRemaining());
        
        
    }

    private IEnumerator CheckEnemiesRemaining()
    {
        while (enemiesRemaining > 0) yield return null;
        AllEnemiesKilled?.Invoke();
    }

    private void EnemyDied()
    {
        
    }
}
