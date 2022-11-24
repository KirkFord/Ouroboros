using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    private InteractionManager _iM;
    private bool _isInShopZone;
    
    private Player _player;
    [SerializeField] private Animator shopKeepAnim;
    [SerializeField] private Animator cameraAnim;
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Proj_Slash projSlash;
    [SerializeField] private Proj_Magic_Shoot projWand;

    private bool _isShopOpen;
    private static readonly int ShopView = Animator.StringToHash("ShopView");

    private void Start()
    {
        _player = Player.Instance;
        _player.EnteredShopZone += EnteredZone;
        _player.LeftShopZone += LeftZone;
        _iM = InteractionManager.Instance;
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
        // TODO: check if player has enough coins -- 2coins + loops
        projSlash.IncreaseDamage(10);
    }

    public void IncreaseWandDamageHandler() {
        // TODO: check if player has enough coins -- 3coins + loops
        projWand.IncreaseDamage(15);
    }
}
