using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    private bool opened = false;

    [SerializeField] private string sceneToChangeTo;


    public void TeleportPlayer()
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
    public void OpenDoor() {
        if (!opened) {
            door.Play("HealDoorOpen", 0, 0.0f);
            opened = true;
        }
    }
}
