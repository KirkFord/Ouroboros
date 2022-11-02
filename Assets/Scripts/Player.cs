using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float playerSpeed = 30.0f;
    [SerializeField] float MaxHealth = 100.0f;
    private float CurrentHealth;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        _rb.velocity = new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed);
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
