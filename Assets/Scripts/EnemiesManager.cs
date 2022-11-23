using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject enemy4;
    public event Action EnemyKilled;

    private bool canSpawn;

    public int enemiesToSpawn;

    //[SerializeField] private Vector2 spawnArea;

    [SerializeField] private float spawnTimer;

    private float timer;

    [SerializeField] public int EnemiesSpawned;

    [SerializeField] public int EnemiesMaxOnScreen;
    private GameManager _gM;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _gM = GameManager.Instance;
        _gM.AllEnemiesKilled += LevelEnded;
    }

    private void OnDestroy()
    {
        _gM.AllEnemiesKilled -= LevelEnded;
    }

    // Update is called once per frame
    private void Update()
    {
        SpawnEnemy();
    }

    public void SetUpNextLevel(int enemiesInlevel)
    {
        enemiesToSpawn = enemiesInlevel;
        canSpawn = true;
    }

    private void LevelEnded()
    {
        canSpawn = false;
    }

    private void SpawnEnemy()
    {
        if (!canSpawn) return;
        if (enemiesToSpawn <= 0) return;
        timer -= Time.deltaTime;
        if (!(timer < 0f)) return;
        if (EnemiesSpawned >= EnemiesMaxOnScreen) return;

        var picked = Random.Range(0, 4);
        Vector3 position = GenerateRandomPosition();

        GameObject newEnemy = null;
        switch (picked)
        {
            case 0:
                newEnemy = Instantiate(enemy1);
                break;
            case 1:
                newEnemy = Instantiate(enemy2);
                break;
            case 2:
                newEnemy = Instantiate(enemy3);
                break;
            case 3:
                newEnemy = Instantiate(enemy4);
                break;

        }
        
        //GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;

        EnemiesSpawned += 1;
        enemiesToSpawn -= 1;
        timer = spawnTimer;
    }

    private static Vector3 GenerateRandomPosition()
    {
        var position = new Vector3(Random.Range(-10, 10), 1f,30f);
        return position;
    }

    public void EnemyDied()
    {
        EnemiesSpawned -= 1;
        EnemyKilled?.Invoke();
    }
}
