using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private InteractionManager _im;
    private int _coins;
    public int multiplier = 1;

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
        _im = InteractionManager.Instance;
    }

    public void AddCoins(int c) {
        if (c < 0) {
            return;
        }
        _coins += c * multiplier;
        _im.UpdateCoins();
    }

    public void RemoveCoins(int c) {
        if (c < 0) {
            return;
        }
        _coins -= c;
        _im.UpdateCoins();
    }

    public int GetCoins() {
        return _coins;
    }

    public void ResetCoins()
    {
        _coins = 0;
        _im.UpdateCoins();
    }
}
