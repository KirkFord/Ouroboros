using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    private InteractionManager _iM;
    private CoinManager _cm;
    private GameManager _gm;
    private bool _isInShopZone;
    private Player _player;
    [SerializeField] private Animator shopKeepAnim;
    [SerializeField] private Animator cameraAnim;
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Proj_Slash projSlash;
    [SerializeField] private Proj_Magic_Shoot projWand;
    [SerializeField] private TMPro.TextMeshProUGUI swordUpgradeText;
    [SerializeField] private TMPro.TextMeshProUGUI wandUpgradeText;

    private bool _isShopOpen;
    private static readonly int ShopView = Animator.StringToHash("ShopView");

    private void Start()
    {
        _player = Player.Instance;
        _player.EnteredShopZone += EnteredZone;
        _player.LeftShopZone += LeftZone;
        _iM = InteractionManager.Instance;
        _cm = CoinManager.Instance;
        _gm = GameManager.Instance;
        CloseShop();
    }

    private void OnDestroy()
    {
        _player.EnteredShopZone -= EnteredZone;
        _player.LeftShopZone -= LeftZone;
    }

    private void Update()
    {
        if (_isInShopZone && Input.GetKeyDown(KeyCode.F) && !_isShopOpen)
        {
            OpenShop();
        }

        else if (_isShopOpen && Input.GetKeyDown(KeyCode.F))
        {
            CloseShop();   
        }
        
    }

    private void OpenShop()
    {
        int swordCost = 2 + _gm.GetLoops();
        int wandCost = 3 + _gm.GetLoops();
        swordUpgradeText.text = "+10 Sword Damage\n(" + swordCost.ToString() + " Coins)";
        wandUpgradeText.text = "+15 Magic Wand Damage\n(" + wandCost.ToString() + " Coins)";
        shopKeepAnim.Play("Wave",0,0);
        cameraAnim.SetBool(ShopView, true);
        _player.DisableMovement();
        _iM.HideInteractText();
        _isShopOpen = true;
        _player.gameObject.SetActive(false);
        shopUI.gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        _player.gameObject.SetActive(true);
        _isShopOpen = false;
        cameraAnim.SetBool(ShopView, false);
        _player.EnableMovement();
        _iM.ShowInteractText("Press [F] to open the shop");
        shopUI.gameObject.SetActive(false);
    }

    private void EnteredZone()
    {
        if (_isShopOpen) return;
        _isInShopZone = true;
        _iM.ShowInteractText("Press [F] to open the shop");
    }

    private void LeftZone()
    {
        _isInShopZone = false;
        _iM.HideInteractText();
    }

    public void IncreaseSwordDamageHandler() {
        int cost = 2 + _gm.GetLoops();
        if (_cm.GetCoins() > cost) {
            projSlash.IncreaseDamage(10);
            _cm.RemoveCoins(cost);
        }
        _iM.UpdateCoins();
    }

    public void IncreaseWandDamageHandler() {
        int cost = 3 + _gm.GetLoops();
        if (_cm.GetCoins() > cost) {
            projWand.IncreaseDamage(15);
        }
        _cm.RemoveCoins(cost);
        _iM.UpdateCoins();
    }
}
