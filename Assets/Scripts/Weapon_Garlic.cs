using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Garlic : MonoBehaviour
{
    private int cdMax = 40;
    private int cdCurrent = 0;
    [SerializeField] private float damage = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            if (cdCurrent <= 0)
            {
                DealDamage(other);
                cdCurrent = cdMax;
            }
            else
            {
                cdCurrent -= 1;
            }
        }
    }

    void DealDamage(Collider other)
    {
        other.GetComponent<Enemy>().TakeDamage(damage, false);
    }
}
