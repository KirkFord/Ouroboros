using System;
using System.Collections;
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
    public GameOverScreen GameOver;
    private int _loops;
    public int enemiesKilled;
    public float timeStart;
    public float timeEnd;
    public float timeElapsed;
    public int minutes;
    public int seconds;
    
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
        enemiesKilled = 0;
        //GameOver = GameOverScreen.Instance;
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
        enemiesKilled = 0;
        timeStart = Time.time;
        timeEnd = 0;
        timeElapsed = 0;
        minutes = 0; 
        seconds = 0;
        if (_player == null)
        {
            return;
        }
        TerrainSpeedDecrease();
        _player.ResetRun();


        //SceneManager.LoadScene("MainHall");
    }

    private void LoadMainHall()
    {
        _player.levelOver = false;
        _player.canAttack = true;
        InteractionManager.Instance.HideInteractText();
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
        if (Math.Abs(terrainMoveSpeed - _player.playerSpeed) < 0) return;
        terrainMoveSpeed = _player.playerSpeed;
    }
    
    public void TerrainSpeedDecrease()
    {
        if (Math.Abs(terrainMoveSpeed - terrainMoveSpeedNormal) < 0) return;
        terrainMoveSpeed = terrainMoveSpeedNormal;
    }

    public void ShowGameOver()
    {
        GameOver.dead();
    }

    public void CalculateTime()
    {
        timeElapsed = timeEnd - timeStart;
        minutes = (int)timeElapsed / 60;
        seconds = (int)timeElapsed % 60;
    }
}

