using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy")]
public class SO_Enemy : ScriptableObject
{
    public float maxHealth = 100.0f;
    public float moveSpeed = 1.0f;
    
    public float damageToPlayer = 5.0f;
    public float damageRate = 0.5f;
    public float damageTime;
    public float damageFlashTimer;

    public float damageScaleFactor;
    public float healthScaleFactor;
}
