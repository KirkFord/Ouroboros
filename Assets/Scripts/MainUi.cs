using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUi : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       gameObject.SetActive(scene.buildIndex is not ((int) Level.StartScreen or (int) Level.LoadManagersLevel
            or (int) Level.LoadPlayerLevel));
    }
}
