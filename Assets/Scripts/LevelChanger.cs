using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level
{
    LoadManagersLevel = 0,
    LoadPlayerLevel = 1,
    MainLevel = 2,
    HealLevel = 3,
    PuzzleLevel1 = 4,
    ShopLevel = 5,
    StartScreen = 6,
    PuzzleLevel2 = 7
}

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger Instance;
    [SerializeField] private Animator anim;
    private Level _levelToLoad;
    

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
        //Debug.Log("Loading Scene: " + scene.name);
        const int loadManagers = (int)Level.LoadManagersLevel;
        const int loadPlayer = (int)Level.LoadPlayerLevel;
        if (scene.buildIndex is loadManagers or loadPlayer) return; 
        //if the loaded scene is the manager scene
        anim.Play("Fade_In");
    }
    
    public void FadeToLevel(Level levelName)
    {
        _levelToLoad = levelName;
        anim.Play("Fade_Out");
    }

    public void FadeDone()
    {
        SceneManager.LoadScene((int)_levelToLoad);
    }

    public Level GetLevel() 
    {
        return (Level)SceneManager.GetActiveScene().buildIndex;
    } 
}
