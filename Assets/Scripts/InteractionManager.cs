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
    
    [Header("Health Bar")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthFill;
    [SerializeField] private Gradient grad;
    [SerializeField] private TMP_Text healthText;

    [Header("XpBar")] 
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TMP_Text levelText;
    
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
        coins.text = _cm.GetCoins().ToString();
        healthFill.color = grad.Evaluate(1f);
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
        coins.text = _cm.GetCoins().ToString();
    }

    public void UpdateHealthBar() {
        healthSlider.value = _player.currentHealth / _player.maxHealth;
        healthText.text = $"{(int)_player.currentHealth}/{(int)_player.maxHealth}";
        healthFill.color = grad.Evaluate(healthSlider.normalizedValue);
    }

    public void UpdateXpBar()
    {
        var valueOfSlider = _player._amountOfXP / _player._XPtoNextlevel;
        if (float.IsNaN(valueOfSlider)) valueOfSlider = 0;
        xpSlider.value = valueOfSlider;
    }

    public void UpdateLevelText()
    {
        levelText.text = $"Lvl: {_player._currentLevel}";
    }

    public void SetPlayer(Player p) {
        _player = p;
    }
}
