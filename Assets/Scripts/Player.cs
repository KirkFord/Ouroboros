using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float playerSpeed = 30.0f;
<<<<<<< Updated upstream
    [SerializeField] float MaxHealth = 100.0f;
    private float CurrentHealth;
=======
    private bool _levelOver;//TODO: REMOVE TRUE, ITS FOR DEBUG
    private GroundController _gc;
>>>>>>> Stashed changes
    private void Start()
    {
        _gc = GameObject.Find("GroundController").GetComponent<GroundController>();
        _rb = GetComponent<Rigidbody>();
<<<<<<< Updated upstream
        CurrentHealth = MaxHealth;
=======

        _gc.AllEnemiesKilled += LevelOver;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DEADZONE")
        {
            Debug.Log("PLAYER ENTER DEADZONE");
        }
>>>>>>> Stashed changes
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
