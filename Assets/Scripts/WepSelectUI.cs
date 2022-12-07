using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepSelectUI : MonoBehaviour
{
    public GameObject wepOverlay;
    
    
    public void Off()
    {
        BGM.Instance.mainHallSwitch();
        wepOverlay.SetActive(false);
    }
    
    public void On()
    {
        BGM.Instance.wepSelectSwitch();
        wepOverlay.SetActive(true);
    }

    public void choice1()
    {
        //change player wep here
        Time.timeScale = 1f;
        GameManager.Instance.LoadMainHall();
    }
    
    public void choice2()
    {
        //change player wep here
        Time.timeScale = 1f;
        GameManager.Instance.LoadMainHall();
    }
    
    public void choice3()
    {
        //change player wep here
        Time.timeScale = 1f;
        GameManager.Instance.LoadMainHall();
    }
}
