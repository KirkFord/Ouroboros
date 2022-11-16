using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger Instance;
    [SerializeField] private Animator anim;
    private int levelToLoad;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) return; //if the loaded scene is the manager scene
        anim.Play("Fade_In");
    }
    
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.Play("Fade_Out");
    }

    public void FadeDone()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
