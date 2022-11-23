using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public static Player Instance;
    private GameManager _gm;
    private Rigidbody _rb;
    public float playerSpeed = 10.0f;
    [SerializeField] float MaxHealth = 100.0f;
    public float CurrentHealth;
    private bool _levelOver;
    private GroundController _gc;
    private bool _canMove = true;
    public bool canAttack;
    private Animator animator;
    [SerializeField] float rotateSpeed;
    private BGM bgm;
    public bool DiedOnce;


    public event Action EnteredShopZone;
    public event Action LeftShopZone;
    public event Action EnteredDoorZone;
    public event Action LeftDoorZone;
    
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
        if (SceneManager.GetActiveScene().name == "MainHall") {
            _gm = GameManager.Instance;
            _gm.AllEnemiesKilled += LevelOver;
        } 
        else {
            _levelOver = true;
            canAttack = false;
        }
        animator = GetComponent<Animator>();
        bgm = BGM.instance;
        DiedOnce = false;
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DoorInteractZone"))
        {
            EnteredDoorZone?.Invoke();
        }
        if (other.gameObject.CompareTag("ShopKeeperInteractZone"))
        {
            EnteredShopZone?.Invoke();
        }

        if (other.gameObject.name == "DEADZONE")
        {
            Debug.Log("PLAYER ENTER DEADZONE");
        }
        if (other.transform.CompareTag("Pickup") && other.GetComponent<Collider>().GetType() == typeof(CapsuleCollider))
        {
            //add some points or something
            //GetComponent<Player>().PlayerAddPoints();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorInteractZone"))
        {
            LeftDoorZone?.Invoke();
        }
        if (other.CompareTag("ShopKeeperInteractZone"))
        {
            LeftShopZone?.Invoke();
        }
    }

    private void Update()
    {
        // if (_levelOver) return;
        transform.Translate(new Vector3(0,0, -_gm.terrainMoveSpeed * Time.deltaTime));
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
        
        _rb.velocity = new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed);

        if (_rb.velocity != Vector3.zero)
        {
            //transform.forward = _rb.velocity;
            Quaternion toRotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    private void LevelOver()
    {
        _levelOver = true;
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
            Debug.Log("this dude is dead");
        }
        Debug.Log("player taking damage");
    }

    public void Death()
    {
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
}
