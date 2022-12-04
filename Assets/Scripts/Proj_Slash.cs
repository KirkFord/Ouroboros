using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Slash : MonoBehaviour
{
    [SerializeField] private float _damage = 75f;
    [SerializeField] private float lifeTime = 2.0f;
    private float critChance = 0.3f;
    private float minCritBonus = 0.25f;
    private float maxCritBonus = 0.76f;
    //[SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
        //transform.SetParent(player.transform, false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Snap();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            float bonusCritDamage = 0;
            bool isCrit = false;
            if (Random.Range(0.0f, 1.0f) < critChance) {
                bonusCritDamage = _damage * Random.Range(minCritBonus, maxCritBonus);
                isCrit = true;
            }
            other.GetComponent<Enemy>().TakeDamage(_damage + bonusCritDamage, isCrit);
        }
    }

    // Snaps position to where we need it to be every frame
    void Snap()
    {
        transform.localPosition = new Vector3(0, 0, 0); // Vector value is arbitrary
    }

    public void IncreaseDamage(float d) {
        this._damage += d;
    }
}