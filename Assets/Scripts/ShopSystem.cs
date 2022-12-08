using UnityEngine;

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
    [SerializeField] private TMPro.TextMeshProUGUI garlicUpgradeText;
    [SerializeField] private TMPro.TextMeshProUGUI oRingUpgradeText;

    private int baseSwordPurchaseCost = 50;
    private int baseWandPurchaseCost = 50;
    private int baseGarlicPurchaseCost = 50;
    private int baseORingPurchaseCost = 500;

    private int SwordUpgradeCost;
    private int WandUpgradeCost;
    private int GarlicUpgradeCost;
    private int ORingUpgradeCost;
    

    private bool _isShopOpen;
    private static readonly int ShopView = Animator.StringToHash("ShopView");

    private void Start()
    {
        _player = Player.Instance;
        _iM = InteractionManager.Instance;
        _cm = CoinManager.Instance;
        _gm = GameManager.Instance;
        shopUI.gameObject.SetActive(false);
        _isShopOpen = false;
        _iM.HideInteractText();
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
        RefreshButtonText();
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

    public void EnteredZone()
    {
        if (_isShopOpen) return;
        _isInShopZone = true;
        _iM.ShowInteractText("Press [F] to open the shop");
    }

    public void LeftZone()
    {
        _isInShopZone = false;
        _iM.HideInteractText();
    }

    public void IncreaseSwordDamageHandler()
    {
        if (_player.hasWinterhorn)
        {
            if (_cm.GetCoins() < SwordUpgradeCost) return;
            Player.Instance.winterhornAttack.IncreaseDamage(10);
            _cm.RemoveCoins(SwordUpgradeCost);
            _iM.UpdateCoins();
        }
        else
        {
            if (_cm.GetCoins() < baseSwordPurchaseCost) return;
            Player.Instance.hasWinterhorn = true;
            _cm.RemoveCoins(baseSwordPurchaseCost);
            _iM.UpdateCoins();
        }
        Player.Instance.winterhornUpgradesPurchased += 1;
        RefreshButtonText();
    }

    public void IncreaseWandDamageHandler()
    {
        if (_player.hasLichTorch)
        {
            if (_cm.GetCoins() < WandUpgradeCost) return;
            Player.Instance.lichTorchAttack.IncreaseDamage(10);
            _cm.RemoveCoins(WandUpgradeCost);
            _iM.UpdateCoins();
        }
        else
        {
            if (_cm.GetCoins() < baseWandPurchaseCost) return;
            Player.Instance.hasLichTorch = true;
            _cm.RemoveCoins(baseWandPurchaseCost);
            _iM.UpdateCoins();
        }
        Player.Instance.lichTorchUpgradesPurchased+= 1;
        RefreshButtonText();
    }

    public void IncreaseGarlicDamageHandler()
    {
        if (_player.hasSilverlight)
        {
            if (_cm.GetCoins() < GarlicUpgradeCost) return;
            Player.Instance.silverlight.AddDamage(10);
            _cm.RemoveCoins(GarlicUpgradeCost);
            _iM.UpdateCoins();
        }
        else
        {
            if (_cm.GetCoins() < baseGarlicPurchaseCost) return;
            Player.Instance.hasSilverlight = true;
            _cm.RemoveCoins(baseGarlicPurchaseCost);
            _iM.UpdateCoins();
        }
        Player.Instance.silverlightUpgradesPurchased += 1;
        RefreshButtonText();
    }

    public void ORingHandler(){

        if (!_player.hasORing)
        {
            if (_cm.GetCoins() < baseORingPurchaseCost) return;
            _player.IncreaseLifesteal(0.01f);
            Player.Instance.ORingUpgradesPurchased += 1;
            Player.Instance.hasORing = true;
            _cm.RemoveCoins(baseORingPurchaseCost);
            _iM.UpdateCoins();
        }
        else
        {
            if (_cm.GetCoins() < ORingUpgradeCost) return;
            _player.IncreaseLifesteal(0.01f);
            _cm.RemoveCoins(ORingUpgradeCost);
            _iM.UpdateCoins(); 
        }
        Player.Instance.ORingUpgradesPurchased += 1;
        RefreshButtonText();
    }


    private void RefreshButtonText()
    { 
        SwordUpgradeCost = 5 * Player.Instance.winterhornUpgradesPurchased;
        WandUpgradeCost = 5 * Player.Instance.lichTorchUpgradesPurchased;
        GarlicUpgradeCost = 5 * Player.Instance.silverlightUpgradesPurchased;
        ORingUpgradeCost = 100 * Player.Instance.ORingUpgradesPurchased;
        
        swordUpgradeText.text = !Player.Instance.hasWinterhorn ? 
            $"Purchase Winterhorn ({baseSwordPurchaseCost} Coins)" 
            : 
            $"+7 Winterhorn Damage ({SwordUpgradeCost} Coins)";
        
        wandUpgradeText.text = !Player.Instance.hasLichTorch ? 
            $"Purchase The Lich Torch ({baseWandPurchaseCost} Coins)" 
            : 
            $"+10 Lich Torch Damage ({WandUpgradeCost} Coins)";
        
        garlicUpgradeText.text = !Player.Instance.hasSilverlight ? 
            $"Purchase Silverlight ({baseGarlicPurchaseCost} Coins)" 
            : 
            $"+5 AOE Damage ({GarlicUpgradeCost} Coins)";

        oRingUpgradeText.text = !Player.Instance.hasORing
            ? $"Purchase the Ouroboros Ring\n0.1% Life Steal ({baseORingPurchaseCost} Coins)"
            : 
            $"+0.1% Lifesteal ({ORingUpgradeCost} Coins)";
    }
}
