using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{
    private Transform targetDestination;
    private GameObject targetGameObject;
    [SerializeField] private float speed;
    [SerializeField] float MaxHealth = 100.0f;
    private float CurrentHealth;
    private Rigidbody rgbd;
    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }
    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgbd.velocity = direction * speed;
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("attacking");
    }

    private void TakeDamage(float damage)
    {
        Debug.Log("enemy taking damage");
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(this.GameObject());
            EnemiesManager.instance.EnemiesSpawned -= 1;
        }
    }
}
