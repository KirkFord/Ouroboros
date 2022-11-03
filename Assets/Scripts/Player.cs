using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float playerSpeed = 30.0f;
    [SerializeField] float MaxHealth = 100.0f;
    private float CurrentHealth;
    private bool _levelOver;//TODO: REMOVE TRUE, ITS FOR DEBUG
    private GroundController _gc;
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "HealingRoom") {
            _gc = GameObject.Find("GroundController").GetComponent<GroundController>();
            _gc.AllEnemiesKilled += LevelOver;
        } else {
            _levelOver = true;
        }
        CurrentHealth = MaxHealth;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DEADZONE")
        {
            Debug.Log("PLAYER ENTER DEADZONE");
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
    private void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Debug.Log("this dude is dead");
        }
        Debug.Log("player taking damage");
    }
    
    
    

}
