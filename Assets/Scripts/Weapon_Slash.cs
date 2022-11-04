using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Slash : MonoBehaviour
{
    private int _slashCooldown = 20;
    [SerializeField] private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Snap();

        if (_slashCooldown > 0)
        {
            _slashCooldown -= 1;
        }
        else
        {
            Slash();
            _slashCooldown = 250;
        }
    }




    // Snaps position to where we need it to be every frame
    void Snap()
    {
        transform.localPosition = new Vector3(0, 0, 1); // Vector value is arbitrary
    }

    void Slash()
    {
        GameObject slashObj = Instantiate(projectile, transform.position, transform.rotation);
        slashObj.transform.parent = this.transform;
    }
}
