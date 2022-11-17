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
    private Animator animator;
    private bool stopMoving;
    [SerializeField] private GameObject deathEffect;
    

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
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (stopMoving == false)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
            transform.LookAt(player.transform);
        }

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
            stopMoving = true;
            animator.SetBool("isDead",true);
            Invoke("Death",1.33f);

            int randomNumber = UnityEngine.Random.Range(1,3);
            if(randomNumber == 1)
            {
                var loot = Instantiate(LootObject, transform.position, LootObject.transform.rotation);
            }
        }
    }

    public void Death()
    {
        Destroy(this.GameObject());
        _eM.EnemyDied();
        GameObject ded = Instantiate(deathEffect,transform.position,transform.rotation);
        Destroy(ded,1.5f);

    }
}
