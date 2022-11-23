using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationHelper : MonoBehaviour
{

    [SerializeField] private StartScreen ss;
    [SerializeField] private Animator doorAnim;
    
    public void LeavingMain()
    {
        ss.LeavingMain();
    }

    public void ArrivedAtMain()
    {
        ss.ArrivedAtMain();
    }
    
    public void LeavingSettings()
    {
        ss.LeavingSettings();
    }

    public void ArrivedAtSettings()
    {
        ss.ArrivedAtSettings();
    }

    public void OpenTheDoor()
    {
        doorAnim.Play("DoorOpen");
    }

    public void StartTheGame()
    {
        ss.StartGame();
    }
}
