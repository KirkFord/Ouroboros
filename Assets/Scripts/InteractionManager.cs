using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    [SerializeField] private Text interactText;
    private CoinManager _cm;
    private Player _player;
    [SerializeField] private TMP_Text coins;
    [SerializeField] private Slider slider;
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

    private void Start() {
        _cm = CoinManager.Instance;
        coins.text = "Coins: " + _cm.GetCoins();
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

    public void UpdateCoins() {
        coins.text = "Coins: " + _cm.GetCoins();
    }

    public void UpdateHealthBar() {
        slider.value = _player.CurrentHealth / _player.MaxHealth;
    }

    public void SetPlayer(Player p) {
        this._player = p;
    }
}
