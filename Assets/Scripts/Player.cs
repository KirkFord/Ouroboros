using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private GameManager _gm;
    private Rigidbody _rb;
    private InteractionManager _im;
    public float playerSpeed = 10.0f;
    [SerializeField] public float maxHealth = 100.0f;
    public float currentHealth;
    public bool levelOver;
    private GroundController _gc;
    private bool _canMove = true;
    public bool canAttack;
    private Animator _animator;
    [SerializeField] float rotateSpeed;
    private BGM _bgm;
    public bool diedOnce;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsDead = Animator.StringToHash("isDead");


    public event Action EnteredShopZone;
    public event Action LeftShopZone;

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
        DontDestroyOnLoad(gameObject);
        
        _rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }
    private void Start()
    {
        //_levelOver = true;
        //canAttack = false;
        _animator = GetComponent<Animator>();
        _gm = GameManager.Instance;
        _im = InteractionManager.Instance;
        _im.SetPlayer(Instance);
        _gm.AllEnemiesKilled += LevelOver;
        _bgm = BGM.Instance;
        diedOnce = false;
        canAttack = true;
        _gm.timeStart = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShopKeeperInteractZone"))
        {
            EnteredShopZone?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name != "DEADZONE") return;
        PlayerTakeDamage(1f);
        Debug.Log("PLAYER ENTER DEADZONE");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShopKeeperInteractZone"))
        {
            LeftShopZone?.Invoke();
        }
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!_canMove) return;
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            _animator.SetBool(IsMoving,true);
        }
        else if (horizontalInput == 0 && verticalInput == 0)
        {
            _animator.SetBool(IsMoving,false);
        }
        _rb.velocity = levelOver ? new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed) : new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed - _gm.terrainMoveSpeed);
        
        if (_rb.velocity == Vector3.zero) return;
        var toRotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
    }

    private void LevelOver()
    {
        levelOver = true;
        canAttack = false;
    }
    public void PlayerTakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && diedOnce == false)
        {
            diedOnce = true;
            DisableMovement();
            canAttack = false;
            _animator.SetBool(IsDead,true);
            Invoke(nameof(Death),1.33f);
            //Debug.Log("this dude is dead");
        }
        //Debug.Log("player taking damage");
        _im.UpdateHealthBar();
    }

    public void Death()
    {
        _gm.timeEnd = Time.time;
        _gm.CalculateTime();
        _bgm.GameOverSwitch();
        _gm.ShowGameOver();
        //_gm.ResetRun();
    }

    public void Heal(float healAmt) {
        currentHealth += healAmt;
        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
        Debug.Log("Healing player by " + healAmt + "; New HP: " + currentHealth);
        _im.UpdateHealthBar();
    }

    public void EnableMovement()
    {
        _canMove = true;
    }
    public void DisableMovement()
    {
        _canMove = false;
        _rb.velocity = new Vector3(0, 0, 0);
    }

    public void ResetRun()
    {
        _animator.SetBool(IsDead,false);
        EnableMovement();
        diedOnce = false;
        canAttack = true;
        levelOver = false;
        currentHealth = maxHealth;
        _im.UpdateHealthBar();
    }
}