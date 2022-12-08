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
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        Player.Instance.hasSilverlight = false;
        Player.Instance.hasLichTorch = false;
        Player.Instance.hasWinterhorn = true;
        Player.Instance.winterhornUpgradesPurchased = 1;
        Player.Instance.UpdateWeapons();
        //Time.timeScale = 1f;
        GameManager.Instance.LoadMainHall();
    }
    
    public void choice2()
    {
        //change player wep here
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        Player.Instance.hasLichTorch = true;
        Player.Instance.hasSilverlight = false;
        Player.Instance.hasWinterhorn = false;
        Player.Instance.lichTorchUpgradesPurchased = 1;
        Player.Instance.UpdateWeapons();
        //Time.timeScale = 1f;
        GameManager.Instance.LoadMainHall();
    }
    
    public void choice3()
    {
        //change player wep here
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        Player.Instance.hasLichTorch = false;
        Player.Instance.hasWinterhorn = false;
        Player.Instance.hasSilverlight = true;
        Player.Instance.silverlightUpgradesPurchased = 1;
        Player.Instance.UpdateWeapons();
        //Time.timeScale = 1f;
        GameManager.Instance.LoadMainHall();
    }
}
