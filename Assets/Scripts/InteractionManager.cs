using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    [SerializeField] private Text interactText;
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
    }
    
    public void ShowInteractText(string text)
    {
        interactText.text = text;
        interactText.enabled = true;
    }

    public void HideInteractText()
    {
        interactText.enabled = false;
    }
    
}
