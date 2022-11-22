using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    private bool opened = false;

    private LevelChanger _lC;
    private InteractionManager _iM;
    private Player _player;
    private bool _playerInZone;
    [SerializeField] private Level sceneToChangeTo;
    private bool alreadyTeleporting;

    private void Start()
    {
        _lC = LevelChanger.Instance;
        _iM = InteractionManager.Instance;
        _player = Player.Instance;
        _player.EnteredDoorZone += PlayerEnteredZone;
        _player.LeftDoorZone += PlayerLeftZone;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F) || !_playerInZone) return;
        if (alreadyTeleporting) return;
        _iM.HideInteractText();
        TeleportPlayer();
        alreadyTeleporting = true;
    }

    private void PlayerEnteredZone()
    {
        _iM.ShowInteractText("Press [F] to Leave");
        _playerInZone = true;
    }

    private void PlayerLeftZone()
    {
        _iM.HideInteractText();
        _playerInZone = false;
    }

    private void TeleportPlayer() {
        if (_lC.GetLevel() == Level.HealLevel) {
            OpenDoor();
        }
        _lC.FadeToLevel(
            sceneToChangeTo
            );
    }

    public void OpenDoor() {
        if (!opened) {
            door.Play("HealDoorOpen", 0, 0.0f);
            opened = true;
        }
    }
}
