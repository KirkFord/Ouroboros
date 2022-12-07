using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SO_Enemy enemyData;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private Material damageFlash;
    [SerializeField] private LootTable drops;

    private Player _player;
    private Rigidbody _rb;
    private EnemiesManager _eM;
    private GameManager _gM;
    private Animator _animator;
    public GameObject lootObject;
    public GameObject XP;

    private float _damageFlashTimer;
    private Material _originalMaterial;
    private float _moveSpeed;
    private float _currentHealth;
    private bool _collected;
    private bool _diedOnce;
    private float _damageToPlayer;
    private float _damageRate;
    private float _damageTime;
    private bool _stopMoving;

    private float _healthScaleFactor;
    private float _damageScaleFactor;
    
    [SerializeField] private float invincibilityTimer = 0.75f;
    [SerializeField] private bool canTakeDamage;
    
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
        _originalMaterial = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _animator = GetComponent<Animator>();
        _diedOnce = false;

        
        _damageFlashTimer = enemyData.damageFlashTimer;
        _currentHealth = enemyData.maxHealth;
        _moveSpeed = enemyData.moveSpeed;
        
        _damageToPlayer = enemyData.damageToPlayer;
        _damageRate = enemyData.damageRate;
        _damageTime = enemyData.damageTime;

        _healthScaleFactor = enemyData.healthScaleFactor;
        _damageScaleFactor = enemyData.damageScaleFactor;
        
        canTakeDamage = true;

        
        ScaleStats();
    }

    private void ScaleStats()
    {
        var loops = GameManager.Instance.GetLoops();
        //Gets Bigger and Bigger.
        _currentHealth += math.pow(loops, 2) * _healthScaleFactor;
        //Eventually will plateau, just so enemies don't 100% one shot you late game.
        _damageToPlayer *= 1 * math.pow(math.sqrt(_damageToPlayer * loops),_damageScaleFactor);
       // Debug.Log("Enemy damage scaled to: " + _damageToPlayer);
       // Debug.Log("Enemy health scaled to: " + _currentHealth);
    }
    private void LateUpdate()
    {
        _rb.velocity = new Vector3(0, 0, -_gM.terrainMoveSpeed);
        if (_stopMoving) return;
        transform.position =
            Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
        transform.LookAt(_player.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyOverflow"))
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
        var hit = player.PlayerTakeDamage(_damageToPlayer, false);
        if (hit)
        {
           // Debug.Log("Did " + _damageToPlayer + " to the player."); 
        }
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        if (!canTakeDamage) return;
        DamagePopup.Create(transform.position, ((int)damage).ToString(), isCrit);
        if (_player.GetLifesteal() > 0) {
           // Debug.Log("Healing player by " + damage * _player.GetLifesteal());
        }
        _player.Heal(damage * _player.GetLifesteal());
        _currentHealth -= damage;
        StartCoroutine(DamageFlash());
        StartCoroutine(InvincibilityFrames());
        //DEATH
        if (!(_currentHealth <= 0) || _diedOnce) return;
        _diedOnce = true;
        _stopMoving = true;
        //<BoxCollider>().isTrigger = false;
        _damageToPlayer = 0f;
        _animator.SetBool(IsDead,true);
        Invoke(nameof(Death),1.33f);
        
        Instantiate(XP, transform.position, XP.transform.rotation);

        var randomNumber = Random.Range(1,3);
        if(randomNumber == 1)
        {
            Instantiate(lootObject, transform.position, lootObject.transform.rotation);
        }
        var reward = drops.GetRandomItem();
        
        if (reward.itemName.Equals("None"))
        {
            return;
        }
        var newPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Instantiate(reward.drop, newPosition, reward.drop.transform.rotation);
        
        
    }

    private IEnumerator DamageFlash()
    {
        foreach (var smr in  GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            smr.material = damageFlash;
        }
        yield return new WaitForSeconds(_damageFlashTimer);
        
        foreach (var smr in  GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            smr.material = _originalMaterial;
        }
    }
    public void Death()
    {
        _eM.EnemyDied();
        var ded = Instantiate(deathEffect,transform.position,transform.rotation);
        Destroy(ded,1.5f);
        Destroy(gameObject);
    }
    private IEnumerator InvincibilityFrames()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityTimer);
        canTakeDamage = true;
    }
}
