using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    private bool opened = false;
    private enum Level
    {
        LoadManagersLevel = 0,
        MainLevel = 1,
        HealLevel = 2,
        PuzzleLevel = 3,
        ShopLevel = 4
    }

    private LevelChanger _lC;
    private InteractionManager _iM;
    private Player _player;
    private bool _playerInZone;
    [SerializeField] private Level sceneToChangeTo;

    private void Start()
    {
        _lC = LevelChanger.Instance;
        _iM = InteractionManager.Instance;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _player.EnteredDoorZone += PlayerEnteredZone;
        _player.LeftDoorZone += PlayerLeftZone;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F) || !_playerInZone) return;
        _iM.HideInteractText();
        _lC.FadeToLevel((int)sceneToChangeTo);
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

    public void TeleportPlayer() {
        _lC.FadeToLevel((int)sceneToChangeTo);
    }

    public void OpenDoor() {
        if (!opened) {
            door.Play("HealDoorOpen", 0, 0.0f);
            opened = true;
        }
    }
}
