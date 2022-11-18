using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private GameManager _gm;
    private Rigidbody _rb;
    public float playerSpeed = 10.0f;
    [SerializeField] float MaxHealth = 100.0f;
    private float CurrentHealth;
    private bool _levelOver;
    private GroundController _gc;
    private bool _canMove = true;
    private bool _canAttack;
    private Animator animator;
    [SerializeField] float rotateSpeed;

    public event Action EnteredShopZone;
    public event Action LeftShopZone;
    public event Action EnteredDoorZone;
    public event Action LeftDoorZone;
    
    private void Awake()
    {
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
        }
        animator = GetComponent<Animator>();
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

        // if this {var=5} elif !this {var=10} -> this ? var=5 : var=10
        // only works when assigning a variable
        _rb.velocity = !_levelOver ? 
            new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed - 3.0f) 
            : 
            new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed);
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
        _canAttack = false;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            DisableMovement();
            animator.SetBool("isDead",true);
            Invoke("Death",1.33f);
            Debug.Log("this dude is dead");
        }
        Debug.Log("player taking damage");
    }

    public void Death()
    {
        _gm.ResetRun();
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
