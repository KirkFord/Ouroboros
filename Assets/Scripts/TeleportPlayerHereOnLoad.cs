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
        _player.gameObject.transform.position = transform.position;
    }
}
