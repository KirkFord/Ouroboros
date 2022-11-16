using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    private InteractionManager _iM;
    private bool _isInShopZone;
    
    [SerializeField] private Player player;
    [SerializeField] private Animator shopKeepAnim;
    [SerializeField] private Animator cameraAnim;

    private bool _isShopOpen;
    private static readonly int ShopView = Animator.StringToHash("ShopView");

    private void Start()
    {
        player.EnteredShopZone += EnteredZone;
        player.LeftShopZone += LeftZone;
        _iM = InteractionManager.Instance;
    }

    private void OnDestroy()
    {
        player.EnteredShopZone -= EnteredZone;
        player.LeftShopZone -= LeftZone;
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
        player.DisableMovement();
        _iM.HideInteractText();
        _isShopOpen = true;
        player.gameObject.SetActive(false);
    }

    private void CloseShop()
    {
        player.gameObject.SetActive(true);
        _isShopOpen = false;
        cameraAnim.SetBool(ShopView, false);
        player.EnableMovement();
        _iM.ShowInteractText("Press [F] to open the shop");
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
}
