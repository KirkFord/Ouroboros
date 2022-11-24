using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Magic_Shoot : MonoBehaviour
{
    private GameObject target;
    private bool hasBeenMade = false;
    private float moveSpeed = 5.0f;
    [SerializeField] private float damage = 5000.0f;
    private GameManager _gm;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.Instance;
        _player = Player.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasBeenMade)
        {
            Move();
        }
    }

    void Move()
    {
        float zSpeedAdjustment = 0; // must adjust movement along z axis to make the projectile appear to
                                    // move relative to the ground
        if (Math.Abs(_gm.terrainMoveSpeed - _player.playerSpeed) < 0.01) {
            zSpeedAdjustment = _player.playerSpeed - 3;
            //Debug.Log("Adjusting wand projectile speed: " + zSpeedAdjustment);
        }
        if (target == null) {
            Destroy(this.gameObject);
            return;
        }
        transform.LookAt(target.transform);
        transform.position += transform.forward * moveSpeed * Time.deltaTime 
                                - new Vector3(0, 0, zSpeedAdjustment) * Time.deltaTime;
    }

    public void Created(GameObject passTarget)
    {
        target = passTarget;
        hasBeenMade = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void IncreaseDamage(float d) {
        
        this.damage += d;
    }
}
