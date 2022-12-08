using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Garlic : MonoBehaviour
{
    private int cdMax = 40;
    private int cdCurrent = 0;
    [SerializeField] private float damage = 50.0f;
    private int weaponId = 2;
    private List<Collider> collList;
    // Start is called before the first frame update

    void Start()
    {
        collList = new List<Collider>();
    }


    public void AddDamage(float extraDamage)
    {
        damage += extraDamage;
    }

    public void FixedUpdate()
    {
        if (cdCurrent <= 0)
        {
            DealDamage();
            cdCurrent = cdMax;
        }
        else
        {
            cdCurrent -= 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            collList.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            collList.Remove(other);
        }
    }

    void DealDamage()
    {
        for (int i = 0; i < collList.Count; i++)
        {
            if (collList[i] == null)
            {
                collList.Remove(collList[i]);
            }
            else
            {
                collList[i].GetComponent<Enemy>().TakeDamage(damage, false, weaponId);
            }
        }
    }
}
