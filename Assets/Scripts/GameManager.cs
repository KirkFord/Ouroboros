using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action AllEnemiesKilled;
    [SerializeField]private int enemiesRemaining = 10;
    public float terrainMoveSpeed = 3.0f;


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
    }

    private void Start()
    {
        StartCoroutine(CheckEnemiesRemaining());
    }

    private IEnumerator CheckEnemiesRemaining()
    {
        while (enemiesRemaining > 0) yield return null;
        AllEnemiesKilled?.Invoke();
    }
}
