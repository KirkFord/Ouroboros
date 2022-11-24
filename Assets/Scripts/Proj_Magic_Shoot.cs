using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Magic_Shoot : MonoBehaviour
{
    private GameObject target;
    private bool hasBeenMade = false;
    private float moveSpeed = 5.0f;
    [SerializeField] private float damage = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.LookAt(target.transform);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
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
