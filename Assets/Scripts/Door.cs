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
    private bool _alreadyTeleporting = false;

    private void Start()
    {
        _lC = LevelChanger.Instance;
        _iM = InteractionManager.Instance;
        _player = Player.Instance;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F) || !_playerInZone) return;
        if (_alreadyTeleporting) return;
        _iM.HideInteractText();
        TeleportPlayer();
        _alreadyTeleporting = true;
    }

    public void PlayerEnteredZone()
    {
        _iM.ShowInteractText("Press [F] to Leave");
        _playerInZone = true;
    }

    public void PlayerLeftZone()
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

    public void SetDoorPath(Level level)
    {
        sceneToChangeTo = level;
    }
}
