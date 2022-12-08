using UnityEngine;

public class chonkreward : MonoBehaviour
{
    private void OnDestroy()
    {
        var chonkReward = 50 * (GameManager.Instance.GetLoops() / 4);
        CoinManager.Instance.AddCoins(chonkReward);
        DamagePopup.CreatePlayerPopup(Player.Instance.transform.position, $"+{chonkReward}");
    }
}
