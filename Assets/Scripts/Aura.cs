using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    private BGM _bgm;

    private Player _player;

    public GameObject iceRay;
    public GameObject rune;
    public GameObject circle;
    public GameObject pointLight;
    
    // Start is called before the first frame update
    void Start()
    {
        _bgm = BGM.Instance;
        _player = Player.Instance;
        iceRay.transform.SetParent(_player.gameObject.transform);
        rune.transform.SetParent(_player.gameObject.transform);
        circle.transform.SetParent(_player.gameObject.transform);
        pointLight.transform.SetParent(_player.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
