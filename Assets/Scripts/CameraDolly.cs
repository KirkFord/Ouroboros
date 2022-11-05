using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraDolly : MonoBehaviour
{
    private double _finalCameraPos;
    private bool _followingPlayer;
   
    [Tooltip("The End Platform Object in the Scene (MAKE SURE IT IS DISABLED)")]
    [SerializeField] private GameObject end;
    
    [Tooltip("The wall that is disabled in the end prefab")]
    [SerializeField] private GameObject endWall;

    private bool _cameraStopped;

    public void CheckMovement()
    {
        if (!_followingPlayer) return;
        if (transform.position.z >= _finalCameraPos) CameraReachedEnd();
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,2,(float)_finalCameraPos),0.2f);
    }
    public void FollowPlayer()
    {
        _finalCameraPos = Math.Floor(end.transform.position.z);
        _followingPlayer = true;
    }

    private void CameraReachedEnd()
    {
        switch (_cameraStopped)
        {
            case true:
                return;
            case false:
                _cameraStopped = true;
                endWall.SetActive(true);
                break;
        }
    }
}
