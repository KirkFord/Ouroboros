using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private GameManager _gm;
    private Rigidbody _rb;
    private InteractionManager _im;
    public float playerSpeed = 10.0f;
    [SerializeField] public float maxHealth = 100.0f;
    public float currentHealth;
    public bool levelOver;
    private GroundController _gc;
    private bool _canMove = true;
    public bool canAttack;
    private Animator _animator;
    [SerializeField] float rotateSpeed;
    private BGM _bgm;
    public bool diedOnce;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    [SerializeField] private float invincibilityTimer = 0.75f;
    [SerializeField] private bool canTakeDamage;
    private bool _playerAtEnd;
    public event Action<float> TookDamage;
    private float _lifesteal;
    private float _attackSpeedModifier = 0f;
    
    private float _baseXPperLevel;
    public float _amountOfXP;
    public float _XPtoNextlevel;
    public int _currentLevel { get; private set; }
    

    [SerializeField] private Material invincibleMat;
    [SerializeField] private Material normalMat;
    [SerializeField] private SkinnedMeshRenderer playerSMR;

    private bool hasAttackSpeed;
    private bool hasInvincibility;
    private bool hasCoinMultiplier;
    
    public int silverlightUpgradesPurchased;
    public int winterhornUpgradesPurchased;
    public int lichTorchUpgradesPurchased;
    public int ORingUpgradesPurchased;
    
    public Weapon_Garlic silverlight;
    [SerializeField] private Weapon_Slash winterhorn;
    [SerializeField] private Weapon_Magic_Shoot lichTorch;

    public Proj_Slash winterhornAttack;
    public Proj_Magic_Shoot lichTorchAttack;
    
    public bool hasSilverlight;
    public bool hasWinterhorn;
    public bool hasLichTorch;
    public bool hasORing;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Destroying Extra Singleton, name: " + name);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        _rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        //_levelOver = true;
        //canAttack = false;
        _animator = GetComponent<Animator>();
        _gm = GameManager.Instance;
        _im = InteractionManager.Instance;
        _im.SetPlayer(Instance);
        _gm.AllEnemiesKilled += LevelOver;
        _bgm = BGM.Instance;
        diedOnce = false;
        canAttack = true;
        _gm.timeStart = Time.time;
        canTakeDamage = true;
        _lifesteal = 0.0f;
        _amountOfXP = 0.0f;
        _baseXPperLevel = 10.0f;
        _currentLevel = 1;
        _XPtoNextlevel = 10;
        hasSilverlight = false;
        hasWinterhorn = false;
        hasLichTorch = false;
        silverlightUpgradesPurchased = 0;
        winterhornUpgradesPurchased = 0;
        lichTorchUpgradesPurchased = 0;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!_canMove) return;
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            _animator.SetBool(IsMoving, true);
        }
        else if (horizontalInput == 0 && verticalInput == 0)
        {
            _animator.SetBool(IsMoving, false);
        }

        _rb.velocity = levelOver
            ? new Vector3(horizontalInput * playerSpeed, _rb.velocity.y, verticalInput * playerSpeed)
            : new Vector3(horizontalInput * playerSpeed, _rb.velocity.y,
                verticalInput * playerSpeed - _gm.terrainMoveSpeed);

        if (_rb.velocity == Vector3.zero) return;
        var toRotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
    }

    public void PlayerAtEnd()
    {
        _playerAtEnd = true;
    }

    public void MainLevelStart()
    {  
        UpdateWeapons();
        transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);
        rotateSpeed = 0;
        _playerAtEnd = false;
        StartCoroutine(CheckOutOfBounds());
        _im.UpdateLevelText();
        _im.UpdateXpBar();
    }

    private void LevelOver()
    {
        levelOver = true;
        canAttack = false;
        rotateSpeed = 720;
    }

    public bool PlayerTakeDamage(float damage, bool ignoreInvincibility)
    {
        if (!canTakeDamage && !ignoreInvincibility) return false;
        currentHealth -= damage;
        if (currentHealth <= 0 && diedOnce == false)
        {
            diedOnce = true;
            DisableMovement();
            canAttack = false;
            _animator.SetBool(IsDead, true);
            Invoke(nameof(Death), 1.33f);
        }

        _im.UpdateHealthBar();
        StartCoroutine(InvincibilityFrames());
        TookDamage?.Invoke(damage);
        return true;
    }

    public void IncreaseLifesteal(float incr)
    {
        _lifesteal += 0.05f;
    }

    public float GetLifesteal()
    {
        return _lifesteal;
    }

    private IEnumerator CheckOutOfBounds()
    {
        while (!levelOver)
        {
            if (transform.position.z <= -7) PlayerTakeDamage(1f, true);
            yield return null;
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityTimer);
        canTakeDamage = true;
    }

    public void Death()
    {
        _gm.timeEnd = Time.time;
        _gm.CalculateTime();
        _bgm.GameOverSwitch();
        _gm.ShowGameOver();
        EnemiesManager.Instance.enemiesToSpawn = 0;
        EnemiesManager.Instance.Murder();
    }

    public void Heal(float healAmt)
    {
        if (healAmt == 0) return;
        currentHealth += healAmt;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        _im.UpdateHealthBar();
    }

    public void EnableMovement()
    {
        _canMove = true;
    }

    public void DisableMovement()
    {
        _canMove = false;
        _rb.velocity = new Vector3(0, 0, 0);
    }

    public void ResetRun()
    {
        _animator.SetBool(IsDead, false);
        EnableMovement();
        diedOnce = false;
        canAttack = true;
        levelOver = false;
        currentHealth = maxHealth;
        _im.UpdateHealthBar();
        canTakeDamage = true;
        _lifesteal = 0.0f;
        _amountOfXP = 0.0f;
        _baseXPperLevel = 10.0f;
        _currentLevel = 1;
        _XPtoNextlevel = 10;
        hasSilverlight = false;
        hasWinterhorn = false;
        hasLichTorch = false;
        silverlightUpgradesPurchased = 0;
        winterhornUpgradesPurchased = 0;
        lichTorchUpgradesPurchased = 0;
        UpdateWeapons();
    }

    public void gainXP(float XPtoGain)
    {
        _amountOfXP += XPtoGain;
        if (_amountOfXP >= _XPtoNextlevel)
        {
            _XPtoNextlevel = nextLevel();
            _amountOfXP = 0;
            //do some level up type beat here
            _gm.ShowLevelup();
            _bgm.PlaySound(Sound.LevelUp);
            _currentLevel++;
            _im.UpdateLevelText();
            //Debug.Log("player is now level " + _currentLevel);
        }
        _im.UpdateXpBar();
    }

    public float nextLevel()
    {
        var exponent = 1.5f;
        return math.floor(_baseXPperLevel * (Mathf.Pow(_currentLevel,exponent)));
    }

    public void ActivateInvincibility()
    {
        if (hasInvincibility)
        {
            StopCoroutine(InvincibilityPickup());
        }
        hasInvincibility = true;
        StartCoroutine(InvincibilityPickup());
        DamagePopup.CreatePlayerPopup(transform.position, "Invincibility (5 Seconds)!");
    }
    public void ActivateCoinMultiplier()
    {
        if (hasCoinMultiplier)
        { 
            StopCoroutine(CoinMultiplierPickup());
        }
        hasCoinMultiplier = true;
        StartCoroutine(CoinMultiplierPickup());
        DamagePopup.CreatePlayerPopup(transform.position, "2X Coins (5 Seconds)!");
    }
    public void ActivateAttackSpeed()
    {
        if (hasAttackSpeed)
        {
            StopCoroutine(AttackSpeedPickup());
        }
        hasAttackSpeed = true;
        StartCoroutine(AttackSpeedPickup());
        DamagePopup.CreatePlayerPopup(transform.position, "+50% Atk Speed (5 Seconds)!");
    }


    private IEnumerator InvincibilityPickup()
    {
        canTakeDamage = false;
        playerSMR.material = invincibleMat;
        yield return new WaitForSeconds(5f);
        playerSMR.material = normalMat;
        canTakeDamage = true;
        hasInvincibility = false;
    }

    private IEnumerator AttackSpeedPickup()
    {
        var oldAttackSpeed = GetAttackSpeed();
        IncreaseAttackSpeed(.5f);
        yield return new WaitForSeconds(5f);
        _attackSpeedModifier = oldAttackSpeed;
        hasAttackSpeed = false;
    }

    private IEnumerator CoinMultiplierPickup()
    {
        CoinManager.Instance.multiplier = 2;
        yield return new WaitForSeconds(5f);
        CoinManager.Instance.multiplier = 1;
        hasCoinMultiplier = false;
    }
    public void IncreaseAttackSpeed(float amount)
    {
        _attackSpeedModifier += amount;
    }

    public float GetAttackSpeed()
    {
        return _attackSpeedModifier;
    }
    public void UpdateWeapons()
    {
        silverlight.gameObject.SetActive(hasSilverlight);
        lichTorch.gameObject.SetActive(hasLichTorch);
        winterhorn.gameObject.SetActive(hasWinterhorn);
    }
}