using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationHelper : MonoBehaviour
{
    [SerializeField] private Animator cameraAnim;

    public void DoorOpen()
    {
        cameraAnim.Play("FlyThroughDoor");
    }
}
