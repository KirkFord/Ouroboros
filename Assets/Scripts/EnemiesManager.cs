using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject enemy4;
    [SerializeField] private GameObject enemy5;
    public bool spawnedChonk;
    public event Action EnemyKilled;

    private bool _canSpawn;

    public int enemiesToSpawn;

    //[SerializeField] private Vector2 spawnArea;

    [SerializeField] private float spawnTimer;

    private float _timer;

    [SerializeField] public int enemiesSpawned;

    [SerializeField] private int enemiesMaxOnScreen;
    private GameManager _gM;

    private List<GameObject> AliveEnemies;
    // Start is called before the first frame update
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
    }

    private void Start()
    {
        _gM = GameManager.Instance;
        _gM.AllEnemiesKilled += LevelEnded;
        AliveEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        SpawnEnemy();
        spawnChonk();
    }

    public void SetUpNextLevel(int enemiesInLevel)
    {
        enemiesToSpawn = enemiesInLevel;
        enemiesMaxOnScreen = 10 * _gM.GetLoops();
        _canSpawn = true;
        spawnedChonk = false;
    }

    private void LevelEnded()
    {
        _canSpawn = false;
    }

    private void spawnChonk()
    {
        if (_gM.GetLoops() != 0 && _gM.GetLoops() % 4 == 0 && !spawnedChonk)
        {
            enemiesToSpawn += 1;
            spawnedChonk = true;
            var newEnemyPosition = GenerateRandomPosition();
            var newEnemy = Instantiate(enemy5);
            if (newEnemy != null) newEnemy.transform.position = newEnemyPosition;
            AliveEnemies.Add(newEnemy);
            enemiesSpawned += 1;
            enemiesToSpawn -= 1;
        }
    }
    
    private void SpawnEnemy()
    {
        if (!_canSpawn) return;
        if (enemiesToSpawn <= 0) return;
        _timer -= Time.deltaTime;
        if (!(_timer < 0f)) return;
        if (enemiesSpawned >= enemiesMaxOnScreen) return;

        var randomEnemyNumber = -1;
        if (_gM.GetLoops() <= 2)
        {
            randomEnemyNumber = 0;
        }
        else if (_gM.GetLoops() > 2 && _gM.GetLoops() <= 4)
        {
            randomEnemyNumber = Random.Range(0,2);
        }
        else if (_gM.GetLoops() > 4 && _gM.GetLoops() <= 6)
        {
            randomEnemyNumber = Random.Range(1,3);

        }
        else if (_gM.GetLoops() > 6 && _gM.GetLoops() <= 8)
        {
            randomEnemyNumber = Random.Range(2,4);
        }
        else if (_gM.GetLoops() > 8)
        {
            randomEnemyNumber = Random.Range(0,4);
            
        }

        var newEnemyPosition = GenerateRandomPosition();

        var newEnemy = randomEnemyNumber switch
        {
            0 => Instantiate(enemy1),
            1 => Instantiate(enemy2),
            2 => Instantiate(enemy3),
            3 => Instantiate(enemy4),
            _ => null
        };

        //GameObject newEnemy = Instantiate(enemy);
        if (newEnemy != null) newEnemy.transform.position = newEnemyPosition;
        AliveEnemies.Add(newEnemy);
        enemiesSpawned += 1;
        enemiesToSpawn -= 1;
        _timer = spawnTimer;
    }

    private static Vector3 GenerateRandomPosition()
    {
        var position = new Vector3(Random.Range(-10, 10), 1f,30f);
        return position;
    }

    public void EnemyDied()
    {
        _gM.enemiesKilled += 1;
        enemiesSpawned -= 1;
        EnemyKilled?.Invoke();
    }

    public void Murder()
    {
        foreach (var enemy in AliveEnemies)
        {
            Destroy(enemy);
        }
    }
}
