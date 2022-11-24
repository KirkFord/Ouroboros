using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private InteractionManager _im;
    private int _coins = 0;

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
        _im = InteractionManager.Instance;
    }

    public void AddCoins(int c) {
        if (c < 0) {
            return;
        }
        _coins += c;
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
}
