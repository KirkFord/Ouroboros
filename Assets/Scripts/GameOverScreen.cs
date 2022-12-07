using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    //public BGM bgm;
    public StatScreen stats;
    //public static bool GamePaused = false;
    public GameObject gameOverUI;
    public GameObject mainUI;

    public void Dead()
    {
        mainUI.GetComponent<MainUi>().resetFlash();
        mainUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void LoadStats(){
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        gameOverUI.SetActive(false);
        BGM.Instance.StatsSwitch();
        stats.ShowStats();
        //Time.timeScale = 1f;
    }
}
