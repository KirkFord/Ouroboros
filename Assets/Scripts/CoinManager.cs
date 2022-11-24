using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private int _coins;

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

    public void AddCoins(int c) {
        if (c < 0) {
            return;
        }
        _coins += c;
    }

    public void RemoveCoins(int c) {
        if (c < 0) {
            return;
        }
        _coins -= c;
    }

    public int GetCoins() {
        return _coins;
    }
}
