using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{
    //private GameManager _gM;
    [SerializeField] private double _finalCameraPos;
    public bool followingPlayer;
    private GameObject _end;
    private GameObject _endWall;

    [SerializeField] private bool _cameraStopped;
    public void CheckMovement()
    {
        //Debug.Log("Checking Player Movement");
        if (transform.position.z >= _finalCameraPos) CameraReachedEnd();
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,2,(float)_finalCameraPos),0.2f);
    }
    public void FollowPlayer()
    {
        _finalCameraPos = Math.Floor(_end.transform.position.z);
        followingPlayer = true;
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
