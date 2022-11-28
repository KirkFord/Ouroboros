using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _gA;

    public static GameAssets i
    {
        get
        {
            if (_gA == null) _gA = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _gA;
        }
    }
    public GameObject damageNumberPrefab;
}
