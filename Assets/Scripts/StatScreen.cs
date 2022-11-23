using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    public GameObject StatUI;
    public TMP_Text enemies;
    public TMP_Text timeSpent;
    public void showStats()
    {
        StatUI.SetActive(true);
        enemies.text = "number of enemies killed: " + GameManager.Instance.enemiesKilled.ToString();
        timeSpent.text = "Time spent in run: " + GameManager.Instance.minutes + "m " + GameManager.Instance.seconds +
                         "s";

        // Time.timeScale = 0f;

    }
    public void LoadMenu(){
        BGM.instance.PlaySound(BGM.Sound.MenuSelectFX);
        StatUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
