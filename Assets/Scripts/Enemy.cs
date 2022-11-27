using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SO_Enemy enemyData;
    [SerializeField] private GameObject deathEffect;
    
    private Player _player;
    private Rigidbody _rb;
    private EnemiesManager _eM;
    private GameManager _gM;
    private Animator _animator;
    public GameObject lootObject;
    
    private float _moveSpeed;
    private float _currentHealth;
    private bool _collected;
    private bool _diedOnce;
    private float _damageToPlayer;
    private float _damageRate;
    private float _damageTime;
    private bool _stopMoving;
    private static readonly int IsDead = Animator.StringToHash("isDead");


    private void Awake()
    {
        _player = Player.Instance;
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _eM = EnemiesManager.Instance;
        _gM = GameManager.Instance;
        _animator = GetComponent<Animator>();
        _diedOnce = false;

        _currentHealth = enemyData.maxHealth;
        _moveSpeed = enemyData.moveSpeed;
        
        _damageToPlayer = enemyData.damageToPlayer;
        _damageRate = enemyData.damageRate;
        _damageTime = enemyData.damageTime;

        
    }

    private void LateUpdate()
    {
        if (_stopMoving) return;
        transform.position =
            Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
        transform.LookAt(_player.transform);
        _rb.velocity = new Vector3(0, 0, -_gM.terrainMoveSpeed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadzone"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 38f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.CompareTag("Player") || !(Time.time > _damageTime)) return;
        Attack(other.transform.GetComponent<Player>());
        _damageTime = Time.time + _damageRate;
    }
    
    private void Attack(Player player)
    {
        player.PlayerTakeDamage(_damageToPlayer);
        Debug.Log("Did " + _damageToPlayer + " to the player.");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("enemy taking damage");
        _currentHealth -= damage;
        if (!(_currentHealth <= 0) || _diedOnce) return;
        _diedOnce = true;
        _stopMoving = true;
        //<BoxCollider>().isTrigger = false;
        _damageToPlayer = 0f;
        _animator.SetBool(IsDead,true);
        Invoke(nameof(Death),1.33f);

        var randomNumber = Random.Range(1,3);
        if(randomNumber == 1)
        {
            Instantiate(lootObject, transform.position, lootObject.transform.rotation);
        }
    }

    public void Death()
    {
        _eM.EnemyDied();
        var ded = Instantiate(deathEffect,transform.position,transform.rotation);
        Destroy(ded,1.5f);
        Destroy(gameObject);
    }
}
