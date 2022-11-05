using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;
    [SerializeField] private GameObject enemy;
    public event Action EnemyKilled;

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

    // Update is called once per frame
    private void Update()
    {
        SpawnEnemy();
    }

    public void SetUpNextLevel(int enemiesInlevel)
    {
        enemiesToSpawn = enemiesInlevel;
    }

    public void SetUpManager()
    {
        _gM = GameManager.Instance;
    }

    private void SpawnEnemy()
    {
        if (enemiesToSpawn <= 0) return;
        timer -= Time.deltaTime;
        if (!(timer < 0f)) return;
        if (EnemiesSpawned >= EnemiesMaxOnScreen) return;

        Vector3 position = GenerateRandomPosition();

        GameObject newEnemy = Instantiate(enemy);
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
