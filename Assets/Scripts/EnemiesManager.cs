using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    //[SerializeField] private Vector2 spawnArea;

    [SerializeField] private float spawnTimer;

    [SerializeField] private GameObject player;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
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
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;
        
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);
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
