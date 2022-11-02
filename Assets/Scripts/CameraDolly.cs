using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraDolly : MonoBehaviour
{
    private double _finalCameraPos;
    private bool _followingPlayer;
    
    [FormerlySerializedAs("_end")]
    [Tooltip("The End Platform Object in the Scene (MAKE SURE IT IS DISABLED)")]
    [SerializeField] private GameObject end;

    public void CheckMovement()
    {
        if (!_followingPlayer) return;
        if (transform.position.z >= _finalCameraPos) return;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,2,(float)_finalCameraPos),0.2f);
    }
    public void FollowPlayer()
    {
        _finalCameraPos = Math.Floor(end.transform.position.z);
        _followingPlayer = true;
    }
}
