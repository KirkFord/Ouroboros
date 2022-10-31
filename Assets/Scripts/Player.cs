using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float playerSpeed = 30.0f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
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
    
        
    
    

}
