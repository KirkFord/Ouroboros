using System;
using UnityEngine;

public class TeleportPlayerHereOnLoad : MonoBehaviour
{
    private Player _player;
    private void Start()
    {
        _player = Player.Instance;
        TeleportPlayerHere();
    }
    
    private void TeleportPlayerHere()
    {
        try
        {
            _player.gameObject.transform.position = transform.position;
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Player doesn't exist so this gameobject will be deleted, note this should only be happening on the start screen");
            Destroy(gameObject);
        }

    }
}
