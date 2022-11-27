using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator door;
    private bool _opened;

    private LevelChanger _lC;
    private InteractionManager _iM;
    private bool _playerInZone;
    [SerializeField] private Level sceneToChangeTo;
    private bool _alreadyTeleporting;

    private void Start()
    {
        _lC = LevelChanger.Instance;
        _iM = InteractionManager.Instance;
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
        if (LevelChanger.GetLevel() == Level.HealLevel) {
            OpenDoor();
        }
        _lC.FadeToLevel(
            sceneToChangeTo
            );
    }

    private void OpenDoor()
    {
        if (_opened) return;
        door.Play("HealDoorOpen", 0, 0.0f);
        _opened = true;
    }

    public void SetDoorPath(Level level)
    {
        sceneToChangeTo = level;
    }
}
