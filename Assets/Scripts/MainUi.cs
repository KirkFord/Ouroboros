using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUi : MonoBehaviour
{
    [SerializeField] private Image damageFlash;
    public GameObject mainUIOverlay;
    
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       gameObject.SetActive(scene.buildIndex is not ((int) Level.StartScreen or (int) Level.LoadManagersLevel
            or (int) Level.LoadPlayerLevel));

       if (scene.buildIndex == (int) Level.LoadPlayerLevel)
       {
           Player.Instance.TookDamage += TookDamage;
       }
    }

    private void TookDamage(float damageTaken)
    {
        if (damageTaken <= 0) return;
        var tempColor = damageFlash.color;
        tempColor.a = 0.7f;
        damageFlash.color = tempColor;
        StartCoroutine(ShowDamageFlash());
    }

    private IEnumerator ShowDamageFlash()
    {
        while (damageFlash.color.a > 0)
        {
            var tempColor = damageFlash.color;
            tempColor.a -= 0.05f;
            damageFlash.color = tempColor;
            yield return null;
        }
    }

    public void resetFlash()
    {
        var damageFlashColor = damageFlash.color;
        damageFlashColor.a = 0;
        damageFlash.color = damageFlashColor;
    }

    public void Off()
    {
        mainUIOverlay.SetActive(false);
    }
    
    public void On()
    {
        mainUIOverlay.SetActive(true);
    }
    

    private void OnEnable()
    {
        resetFlash();
    }
}
