using UnityEngine;

public class Weapon_Slash : MonoBehaviour
{
    private int _slashCooldown = 20;
    private float _cdMax = 30f;
    [SerializeField] private GameObject projectile;
    private BGM bgm;

    private void Start()
    {
        bgm = BGM.Instance;
    }

    private void FixedUpdate()
    {
        if (!Player.Instance.canAttack)
        {
            return;
        }
        Snap();

        if (_slashCooldown > 0)
        {
            _slashCooldown -= 1;
        }
        else
        {
            Slash();
            float attackSpeedMod = Player.Instance.GetAttackSpeed();
            _slashCooldown = (int)(_cdMax / (1 + attackSpeedMod));
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
        bgm.PlaySound(Sound.PlayerSlash);
    }
}
