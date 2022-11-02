using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance = null;
    [SerializeField] private GameObject enemy;

    //[SerializeField] private Vector2 spawnArea;

    [SerializeField] private float spawnTimer;

    [SerializeField] private GameObject player;
    
    private float timer;

    [SerializeField] public int EnemiesSpawned;

    [SerializeField] public int EnemiesMaxOnScreen;
    // Start is called before the first frame update
    void Awake()
    {
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
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    private void SpawnEnemy()
    {
        if (EnemiesSpawned >= EnemiesMaxOnScreen)
        {
            return;
        }
        
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);

        EnemiesSpawned += 1;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(-12, 12);
        position.z = 30f;
        position.y = 0f;

        return position;
    }
}
