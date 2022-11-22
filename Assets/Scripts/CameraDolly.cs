using System;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{
    private double _finalCameraPos;
    private bool _followingPlayer;
    private GameObject _end;
    private GameObject _endWall;

    private bool _cameraStopped;
    
    public void CheckMovement()
    {
        if (!_followingPlayer) return;
        if (transform.position.z >= _finalCameraPos) CameraReachedEnd();
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,2,(float)_finalCameraPos),0.2f);
    }
    public void FollowPlayer()
    {
        _finalCameraPos = Math.Floor(_end.transform.position.z);
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
                _endWall.SetActive(true);
                break;
        }
    }
    public void SetEnd(GameObject endPlatform)
    {
        _end = endPlatform;
        _endWall = _end.transform.GetChild(0).gameObject;
    }
}
