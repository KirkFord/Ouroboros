using TMPro;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _text;
    private Color _textColor;
    private float _lifeTime = 1f;
    private Vector3 _movement;

    private static int _sO;
    
    public static DamagePopup Create(Vector3 location, int damage)
    {
        var damagePopupGameObject = Instantiate(GameAssets.i.damageNumberPrefab, location, quaternion.identity);
        var damagePopup = damagePopupGameObject.GetComponent<DamagePopup>();
        damagePopup.Setup(damage);
        return damagePopup;
    }
    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    private void Setup(int damageAmount)
    {
        _text.SetText(damageAmount.ToString());
        _textColor = _text.color;
        _movement = new Vector3(0, 1, -1.75f) * 2f;
        _sO++;
        _text.sortingOrder = _sO;
    }

    private void Update()
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