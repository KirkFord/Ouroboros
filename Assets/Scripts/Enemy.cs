using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    [SerializeField] private float MaxHealth = 100.0f;
    [SerializeField] private float MoveSpeed = 1.0f;
    private float CurrentHealth;
    private Rigidbody rgbd;
    public GameObject LootObject;
    private bool collected;
    [SerializeField] private float damageToPlayer = 5.0f;
    [SerializeField] private float damageRate = 0.5f;
    [SerializeField] private float damageTime;
    private EnemiesManager _eM;
    private GameManager _gM;
    private Animator animator;
    private bool stopMoving;
    [SerializeField] private GameObject deathEffect;
    

    private void Awake()
    {
        player = Player.Instance;
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
            rgbd.velocity = new Vector3(0, 0, -_gM.terrainMoveSpeed);
        }

    }

    // private void OnCollisionStay(Collision collisionInfo)
    // {
    //     if (collisionInfo.gameObject.name == "Player")
    //     {
    //         Attack();
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadzone"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 38f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && Time.time > damageTime)
        {
            other.transform.GetComponent<Player>().PlayerTakeDamage(damageToPlayer);
            damageTime = Time.time + damageRate;
        }
    }
    
    
    // private void Attack()
    // {
    //     player.GetComponent<Player>().TakeDamage(20f);
    //     Debug.Log("attacking the player");
    // }

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
