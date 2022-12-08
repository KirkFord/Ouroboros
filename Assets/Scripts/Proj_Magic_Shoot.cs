using System;
using UnityEngine;
using Random=UnityEngine.Random;

public class Proj_Magic_Shoot : MonoBehaviour
{
    private GameObject target;
    private bool hasBeenMade = false;
    private float moveSpeed = 10.0f;
    [SerializeField] private float damage = 100.0f;
    private float critChance = 0.3f;
    private float minCritBonus = 0.25f;
    private float maxCritBonus = 0.76f;
    private GameManager _gm;
    private Player _player;
    private int weaponId = 1;

    [SerializeField] private float range = 300.0f;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.Instance;
        _player = Player.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasBeenMade)
        {
            Move();
        }
    }

    void Move()
    {
        float zSpeedAdjustment = 0; // must adjust movement along z axis to make the projectile appear to
                                    // move relative to the ground
        if (Math.Abs(_gm.terrainMoveSpeed - _player.playerSpeed) < 0.01)
        {
            zSpeedAdjustment = _player.playerSpeed - 3;
            //Debug.Log("Adjusting wand projectile speed: " + zSpeedAdjustment);
        }
        //  | target.GetComponent<Enemy>().HasDied() == true
        if (target == null)
        {
            Retarget();
            return;
        }
        else if (target.GetComponent<Enemy>().HasDied())
        {
            Retarget();
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
            float bonusCritDamage = 0;
            bool isCrit = false;
            if (Random.Range(0.0f, 1.0f) < critChance)
            {
                bonusCritDamage = damage * Random.Range(minCritBonus, maxCritBonus);
                isCrit = true;
            }
            other.GetComponent<Enemy>().TakeDamage(damage + bonusCritDamage, isCrit, weaponId);
            Destroy(this.gameObject);
        }
    }

    public void IncreaseDamage(float d)
    {

        this.damage += d;
    }


    bool Retarget()
    {
        bool retVal = false; // return Value
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy"); // All enemies in scene
        // Suicide if no enemies on screen
        if (gos.Length == 0)
        {
            Destroy(this.gameObject);
            return false;
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if(target != null)
            {
                if (go == target)
                {
                    continue;
                }
            }
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
}
