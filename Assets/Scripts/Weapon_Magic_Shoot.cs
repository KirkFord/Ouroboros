using UnityEngine;

public class Weapon_Magic_Shoot : MonoBehaviour
{
    private int _shootCooldown = 30;
    private float _cdMax = 30f;
    [SerializeField] private GameObject projectile;
    private GameObject target;
    [SerializeField] private float range = 300.0f;
    private BGM bgm;

    // Start is called before the first frame update
    void Start()
    {
        bgm = BGM.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Player.Instance.canAttack)
        {
            return;
        }
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
            float attackSpeedMod = Player.Instance.GetAttackSpeed();
            _shootCooldown = (int)(_cdMax / (1 + attackSpeedMod));
            // Debug.Log(attackSpeedMod.ToString());
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
        bgm.PlaySound(Sound.PlayerBolt);
    }
}
