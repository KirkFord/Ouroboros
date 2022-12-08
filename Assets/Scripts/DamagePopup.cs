using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _text;
    private Color _textColor;
    private float _lifeTime = 1f;
    private Vector3 _movement;
    private static int _sO;
    private delegate void MovementVariation();
    
    private MovementVariation _mv;

    public static DamagePopup Create(Vector3 location, string damage, bool isCrit)
    {
        var damagePopupGameObject = Instantiate(GameAssets.i.damageNumberPrefab, location, quaternion.identity);
        var damagePopup = damagePopupGameObject.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, isCrit);
        return damagePopup;
    }
    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    private void Setup(string damageAmount, bool isCrit)
    {
        _mv = MoveDamagePopup;
        _textColor = _text.color;
        _text.fontSize = isCrit ? 70 : 45;
        _text.isTextObjectScaleStatic = false;
        _text.SetText(damageAmount);
        _movement = new Vector3(0, 1, -1.75f) * 2f;
        _sO++;
        _text.sortingOrder = _sO;
    }

    public static DamagePopup CreatePlayerPopup(Vector3 location, string textToDisplay)
    {
        var ppuGO = Instantiate(GameAssets.i.damageNumberPrefab, location, Quaternion.identity);
        var ppu = ppuGO.GetComponent<DamagePopup>();
        ppu.SetupPlayerPopUp(textToDisplay);
        return ppu;
    }

    private void SetupPlayerPopUp(string textToDisplay)
    {
        _mv = MovePlayerPopup;
        _textColor = Color.red;
        _text.fontSize = 70;
        _text.isTextObjectScaleStatic = false;
        _text.SetText(textToDisplay);
        _movement = new Vector3(0, 1, 0) * 2f;
        _sO++;
        _text.sortingOrder = _sO;
    }

    private void Update()
    {
        _mv();
    }

    private void MovePlayerPopup()
    {
        transform.position += _movement * Time.deltaTime;
        _movement -= _movement * Time.deltaTime;

        _lifeTime -= Time.deltaTime;
        if (!(_lifeTime < 0)) return;
        
        _textColor.a -= 3f * Time.deltaTime;
        _text.color = _textColor;
        
        if (_textColor.a < 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveDamagePopup()
    {
        transform.position += _movement * Time.deltaTime;
        transform.Translate(new Vector3(0,0,-GameManager.Instance.terrainMoveSpeed * Time.deltaTime));
        _movement -= _movement * Time.deltaTime;

        _lifeTime -= Time.deltaTime;
        if (!(_lifeTime < 0)) return;
        
        _textColor.a -= 3f * Time.deltaTime;
        _text.color = _textColor;
        
        if (_textColor.a < 0)
        {
            Destroy(gameObject);
        }
    }
    
    
}
