using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float MaxHealth = 100.0f;
    [SerializeField] private float MoveSpeed = 1.0f;
    private float CurrentHealth;
    private Rigidbody rgbd;
    public GameObject LootObject;
    private bool collected;
    private EnemiesManager _eM;
    private GameManager _gM;
    

    private void Awake()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.Find("MaxDistance").GetComponent<Collider>(), true);
        player = GameObject.Find("Player");
        rgbd = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }

    private void Start()
    {
        _eM = EnemiesManager.instance;
        _gM = GameManager.Instance;
    }

    private void LateUpdate()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
    }
    

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.name == "Player")
        {
            Attack();
        }
    }

    private void Attack()
    {
        player.GetComponent<Player>().TakeDamage(20f);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("enemy taking damage");
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(this.GameObject());
            _eM.EnemyDied();

            int randomNumber = UnityEngine.Random.Range(1,3);
            if(randomNumber == 1)
            {
                var loot = Instantiate(LootObject, transform.position, LootObject.transform.rotation);
            }
        }
    }
}
