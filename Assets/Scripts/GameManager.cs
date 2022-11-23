using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player _player;
    private GroundController _gc;
    private EnemiesManager _eM;
    public event Action AllEnemiesKilled;
    [SerializeField] private int _enemiesRemaining;
    public float terrainMoveSpeed = 3.0f;
    [SerializeField] private float terrainMoveSpeedNormal = 3.0f;
    [SerializeField] private float terrainMoveSpeedSpedUp = 6.0f;

    private int _loops;
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        _eM = EnemiesManager.instance;
        _eM.EnemyKilled += EnemyDied;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "LOADPLAYER":
                _player = Player.Instance;
                break;
            case "MainHall":
                LoadMainHall();
                break;
        }
    }

    public void ResetRun()
    {
        _loops = 0;
        _eM.EnemiesSpawned = 0;
        SceneManager.LoadScene("MainHall");
    }

    private void LoadMainHall()
    {
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
    
    public void TerrainSpeedIncrease()
    {
        if (Math.Abs(terrainMoveSpeed - terrainMoveSpeedSpedUp) < 0) return;
        terrainMoveSpeed = terrainMoveSpeedSpedUp;
    }
    
    public void TerrainSpeedDecrease()
    {
        if (Math.Abs(terrainMoveSpeed - terrainMoveSpeedNormal) < 0) return;
        terrainMoveSpeed = terrainMoveSpeedNormal;
    }
    
}

