using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Slash : MonoBehaviour
{
    [SerializeField] private float _damage = 500f;
    [SerializeField] private float lifeTime = 2.0f;
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
            other.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }

    // Snaps position to where we need it to be every frame
    void Snap()
    {
        transform.localPosition = new Vector3(0, 0, 0); // Vector value is arbitrary
    }
}