using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string sceneToChangeTo;

    public void TeleportPlayer()
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
