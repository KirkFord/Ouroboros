using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnLoad : MonoBehaviour
{
    [SerializeField] private Level levelToChangeTo;
    private void Start()
    {
        SceneManager.LoadScene((int) levelToChangeTo);
    }
}
