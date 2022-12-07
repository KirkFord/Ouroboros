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
        catch (NullReferenceException _)
        {
            //Debug.Log("Player doesn't exist so "+ name +" will be deleted, this should only be happening on the start screen\n");
            Destroy(gameObject);
        }

    }
}
