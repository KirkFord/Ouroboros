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
    public event Action LoadingMainLoop;
    
    [SerializeField] private int enemiesRemaining;
    public float terrainMoveSpeed = 3.0f;
    [SerializeField] private float terrainMoveSpeedNormal = 3.0f;
    [SerializeField] private float allEnemiesKilledHeartbeatTimer = 1f;
    public GameOverScreen gameOver;
    public MainUi mainUI;
    public WepSelectUI wepSelect;
    public LevelUpUI LevelUp;
    private int _loops;
    public int enemiesKilled;
    public int dropscollected;
    public float timeStart;
    public float timeEnd;
    public float timeElapsed;
    public int minutes;
    public int seconds;
    public bool walkingToEnd;
    public bool firstTimeWep = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Destroying Extra Singleton, name: " + name);
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        _eM = EnemiesManager.Instance;
        _eM.EnemyKilled += EnemyDied;
        enemiesKilled = 0;
        dropscollected = 0;
        //GameOver = GameOverScreen.Instance;
        StartCoroutine(WaitForPlayer());
    }
    
    private IEnumerator WaitForPlayer()
    {
        while (Player.Instance == null) yield return null;
        _player = Player.Instance;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case (int)Level.MainLevel:
                if (!firstTimeWep)
                {
                    mainUI.Off();
                    wepSelect.On();
                    ShutPlayerUp();
                }
                else
                {
                    LoadMainHall();
                }
                break;
        }
    }

    public void ResetRun()
    {
        dropscollected = 0;
        _loops = 0;
        _eM.enemiesSpawned = 0;
        enemiesKilled = 0;
        timeStart = Time.time;
        timeEnd = 0;
        timeElapsed = 0;
        minutes = 0; 
        seconds = 0;
        firstTimeWep = false;
        if (_player == null) return;
        TerrainSpeedDecrease();
        _player.ResetRun();
        CoinManager.Instance.ResetCoins();
    }

    public void LoadMainHall()
    {
        wepSelect.Off();
        mainUI.On();
        walkingToEnd = false;
        _player.gameObject.SetActive(true);
        _player.levelOver = false;
        _player.canAttack = true;
        _player.EnableMovement();
        TerrainSpeedDecrease();
        InteractionManager.Instance.HideInteractText();
        _loops += 1;
        enemiesRemaining = 30 * _loops;
        _eM.SetUpNextLevel(enemiesRemaining); // buggy
        StartCoroutine(CheckEnemiesRemaining());
        _player.MainLevelStart();
        LoadingMainLoop?.Invoke();
    }

    private void EnemyDied()
    {
        enemiesRemaining -= 1;
    }
    
    private IEnumerator CheckEnemiesRemaining()
    {
        while (enemiesRemaining > 0 || EnemiesManager.Instance.enemiesSpawned > 0) yield return new WaitForSeconds(allEnemiesKilledHeartbeatTimer);
        AllEnemiesKilled?.Invoke();
        walkingToEnd = true;
    }
    
    public void TerrainSpeedIncrease()
    {
        if (Math.Abs(terrainMoveSpeed - _player.playerSpeed) < 0.01) return;
        terrainMoveSpeed = _player.playerSpeed;
    }
    
    public void TerrainSpeedDecrease()
    {
        if (Math.Abs(terrainMoveSpeed - terrainMoveSpeedNormal) < 0) return;
        terrainMoveSpeed = terrainMoveSpeedNormal;
    }

    public void ShowGameOver()
    {
        gameOver.Dead();
    }

    public void CalculateTime()
    {
        timeElapsed = timeEnd - timeStart;
        minutes = (int)timeElapsed / 60;
        seconds = (int)timeElapsed % 60;
    }

    public int GetLoops() {
        return _loops;
    }

    public void ShutPlayerUp()
    {
        _player.canAttack = false;
        _player.DisableMovement();
    }

    public void ShowLevelup()
    {
        LevelUp.On();
    }
    
}

