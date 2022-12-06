using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    //public BGM bgm;
    public StatScreen stats;
    //public static bool GamePaused = false;
<<<<<<< Updated upstream
    public GameObject gameOverUI;
    
    public void Dead()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void LoadStats(){
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        gameOverUI.SetActive(false);
        BGM.Instance.StatsSwitch();
        stats.ShowStats();
        //Time.timeScale = 1f;
=======
    public GameObject GameOverUI;

    // public static GameOverScreen Instance;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // if (Instance == null)
        // {
        //     Instance = this;
        // }
        // else if (Instance != this)
        // {
        //     Destroy(gameObject);
        // }
        
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += dead;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= dead;
    }

    public void dead()
    {
        GameOverUI.SetActive(true);
        // Time.timeScale = 0f;
>>>>>>> Stashed changes
    }
}
