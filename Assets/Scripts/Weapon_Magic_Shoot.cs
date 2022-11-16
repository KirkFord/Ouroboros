using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Magic_Shoot : MonoBehaviour
{
    private int _shootCooldown = 20;
    [SerializeField] private GameObject projectile;
    private GameObject target;
    [SerializeField] private float range = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_shootCooldown > 0)
        {
            _shootCooldown -= 1;
        }
        else
        {
            if (CheckValid())
            {
                Shoot();
            }
            _shootCooldown = 75;
        }
    }

    bool CheckValid()
    {
        bool retVal = false; // return Value
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy"); // All enemies in scene
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < range) // Check within range
            {
                if (curDistance < distance) // Check closest within range
                {
                    closest = go;
                    distance = curDistance;
                }
                retVal = true;
            }
            
        }
        if (retVal) // a target was found, save it
        {
            target = closest;
        }
        return retVal;
    }

    void Shoot()
    {
        GameObject newProj = Instantiate(projectile, transform.position, transform.rotation);
        Proj_Magic_Shoot myscript = newProj.GetComponent<Proj_Magic_Shoot>();
        myscript.Created(target);
    }
}
