using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private GameManager _gm;
    private Rigidbody _rb;
    public float playerSpeed = 30.0f;
    [SerializeField] float MaxHealth = 100.0f;
    private float CurrentHealth;
    private bool _levelOver;
    private GroundController _gc;
    
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            other.gameObject.GetComponent<Door>().TeleportPlayer();
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

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        // if this {var=5} elif !this {var=10} -> this ? var=5 : var=10
        // only works when assigning a variable
        _rb.velocity = !_levelOver ? 
            new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed - 3.0f) 
            : 
            new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed);
    }

    private void LevelOver()
    {
        _levelOver = true;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            _gm.ResetRun();
            Debug.Log("this dude is dead");
        }
        Debug.Log("player taking damage");
    }

    public void Heal(float healAmt) {
        CurrentHealth += healAmt;
        if (CurrentHealth >= MaxHealth) {
            CurrentHealth = MaxHealth;
        }
        Debug.Log("Healing player by " + healAmt + "; New HP: " + CurrentHealth);
    }
}
