using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon_Slash : MonoBehaviour
{
    private int _slashCooldown = 20;
    [SerializeField] private GameObject projectile;
    private BGM bgm;

    private void Start()
    {
        bgm = BGM.instance;
    }

    private void FixedUpdate()
    {
        Snap();

        if (_slashCooldown > 0)
        {
            _slashCooldown -= 1;
        }
        else
        {
            Slash();
            _slashCooldown = 75;
        }
    }
    
    // Snaps position to where we need it to be every frame
    private void Snap()
    {
        transform.localPosition = new Vector3(0, 0, 1); // Vector value is arbitrary
    }

    private void Slash()
    {
        GameObject slashObj = Instantiate(projectile, transform.position, transform.rotation);
        slashObj.transform.parent = this.transform;
        bgm.PlaySound(BGM.Sound.PlayerSlash);
    }
}
