using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private Text interactText;
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
            Debug.Log("HI");
            OpenShop();
        }

        else if (_isShopOpen && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("BYE");
            CloseShop();   
        }
        
    }

    private void OpenShop()
    {
        shopKeepAnim.Play("Wave",0,0);
        cameraAnim.SetBool(ShopView, true);
        player.DisableMovement();
        HideInteractText();
        _isShopOpen = true;
        player.gameObject.SetActive(false);
    }

    private void CloseShop()
    {
        player.gameObject.SetActive(true);
        _isShopOpen = false;
        cameraAnim.SetBool(ShopView, false);
        player.EnableMovement();
        ShowInteractText();
    }

    private void ShowInteractText()
    {
        interactText.text = "Press [F] to open the Shop.";
        interactText.enabled = true;
    }

    private void HideInteractText()
    {
        interactText.enabled = false;
    }

    private void EnteredZone()
    {
        if (_isShopOpen) return;
        _isInShopZone = true;
        ShowInteractText();
    }

    private void LeftZone()
    {
        _isInShopZone = false;
        HideInteractText();
    }
}
