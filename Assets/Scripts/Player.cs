using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public static Player Instance;
    private GameManager _gm;
    private Rigidbody _rb;
    private InteractionManager _im;
    public float playerSpeed = 10.0f;
    [SerializeField] public float MaxHealth = 100.0f;
    public float CurrentHealth;
    public bool levelOver;
    private GroundController _gc;
    private bool _canMove = true;
    public bool canAttack;
    private Animator animator;
    [SerializeField] float rotateSpeed;
    private BGM bgm;
    public bool DiedOnce;


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
        CurrentHealth = MaxHealth;
    }
    private void Start()
    {
        //_levelOver = true;
        //canAttack = false;
        animator = GetComponent<Animator>();
        _gm = GameManager.Instance;
        _im = InteractionManager.Instance;
        _im.SetPlayer(Instance);
        _gm.AllEnemiesKilled += LevelOver;
        bgm = BGM.instance;
        DiedOnce = false;
        canAttack = true;
        _gm.timeStart = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShopKeeperInteractZone"))
        {
            EnteredShopZone?.Invoke();
        }

        if (other.gameObject.name == "DEADZONE")
        {
            Debug.Log("PLAYER ENTER DEADZONE");
        }
        
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
            animator.SetBool("isMoving",true);
        }
        else if (horizontalInput == 0 && verticalInput == 0)
        {
            animator.SetBool("isMoving",false);
        }
        
        _rb.velocity = levelOver ? new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed) : new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed - _gm.terrainMoveSpeed);



        if (_rb.velocity != Vector3.zero)
        {
            //transform.forward = _rb.velocity;
            Quaternion toRotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    private void LevelOver()
    {
        levelOver = true;
        canAttack = false;
    }
    public void PlayerTakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0 && DiedOnce == false)
        {
            DiedOnce = true;
            DisableMovement();
            canAttack = false;
            animator.SetBool("isDead",true);
            Invoke("Death",1.33f);
            //Debug.Log("this dude is dead");
        }
        //Debug.Log("player taking damage");
        _im.UpdateHealthBar();
    }

    public void Death()
    {
        _gm.timeEnd = Time.time;
        _gm.CalculateTime();
        bgm.GameOverSwitch();
        _gm.ShowGameOver();
        //_gm.ResetRun();
    }

    public void Heal(float healAmt) {
        CurrentHealth += healAmt;
        if (CurrentHealth >= MaxHealth) {
            CurrentHealth = MaxHealth;
        }
        Debug.Log("Healing player by " + healAmt + "; New HP: " + CurrentHealth);
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
        animator.SetBool("isDead",false);
        EnableMovement();
        DiedOnce = false;
        canAttack = true;
        levelOver = false;
        CurrentHealth = MaxHealth;
        _im.UpdateHealthBar();
    }
}