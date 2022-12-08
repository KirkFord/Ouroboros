using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpOverlay;
    // Start is called before the first frame update
    
    
    public void Off()
    {
        levelUpOverlay.SetActive(false);
        Time.timeScale = 1f;
        BGM.Instance.musicAudioSource.UnPause();
    }
    
    public void On()
    {
        levelUpOverlay.SetActive(true);
        Time.timeScale = 0f;
    }

    public void choice1()
    {
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        Player.Instance.playerSpeed += (Player.Instance.playerSpeed / 10);
        Off();
    }
    
    public void choice2()
    {
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        Player.Instance.maxHealth += (Player.Instance.maxHealth / 10);
        Time.timeScale = 1f;
        Off();
    }
    
    public void choice3()
    {
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        Player.Instance.IncreaseAttackSpeed(0.1f);
        Time.timeScale = 1f;
        Off();
    }
}
