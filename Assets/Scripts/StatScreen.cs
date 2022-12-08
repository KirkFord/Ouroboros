using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatScreen : MonoBehaviour
{
    public GameObject statUI;
    public TMP_Text enemies;
    public TMP_Text timeSpent;
    public TMP_Text loops;
    public TMP_Text pickups;
    public void ShowStats()
    {
        statUI.SetActive(true);
        enemies.text = "number of enemies killed: " + GameManager.Instance.enemiesKilled.ToString();
        timeSpent.text = "Time spent in run: " + GameManager.Instance.minutes + "m " + GameManager.Instance.seconds +
                         "s";
        loops.text = "number of loops completed: " + (GameManager.Instance.GetLoops() - 1);
        pickups.text = "number of pickups obtained: " + GameManager.Instance.dropscollected;
        
    }
    public void LoadMenu(){
        BGM.Instance.PlaySound(Sound.MenuSelectFX);
        statUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
