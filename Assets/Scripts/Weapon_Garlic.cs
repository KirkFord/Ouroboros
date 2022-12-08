using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Garlic : MonoBehaviour
{
    private int cdMax = 40;
    private int cdCurrent = 0;
    [SerializeField] private float damage = 50.0f;
    private int weaponId = 2;
    // Start is called before the first frame update

    public void AddDamage(float extraDamage)
    {
        damage += extraDamage;
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
        other.GetComponent<Enemy>().TakeDamage(damage, false, weaponId);
    }
}
