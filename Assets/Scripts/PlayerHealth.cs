// Roman Baranov 21.05.2022

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region VARIABLES
    /// <summary>
    /// Max character health
    /// </summary>
    [Min(1)]
    [SerializeField] private int _maxHealth = 1;

    /// <summary>
    /// Maximum player health amount
    /// </summary>
    public int MaxHealth { get { return _maxHealth; } }

    /// <summary>
    /// Current player health amount
    /// </summary>
    public int CurHealth { get; private set; } = 3;

    private bool _isDamagable = true;
    [SerializeField] private float _damageImmuneDuration = 3f;
    private float _curImmuneTimer = 0f;
    #endregion

    #region UNITY Methods
    private void Start()
    {
        CurHealth = _maxHealth;

        // Health state change callbacks
        GameplayEvents.OnPlayerGetHeal.AddListener(GetHealing);
    }

    private void Update()
    {
        DamageImmuneTimer(_damageImmuneDuration);
    }
    #endregion

    #region CALLBACK Handlers
    /// <summary>
    /// Decrease health by given amount
    /// </summary>
    /// <param name="damage">Damage amount</param>
    private void GetDamage(int damage)
    {
        if (_isDamagable)
        {
            CurHealth -= damage;
            _isDamagable = false;
            if (CurHealth <= 0)
            {
                CurHealth = 0;
               
                // Send player death callback
                GameplayEvents.OnPlayerDead.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Increase heath by given amount
    /// </summary>
    /// <param name="value">Healing amount</param>
    private void GetHealing(int value)
    {
        CurHealth += value;
        if (CurHealth > _maxHealth)
        {
            CurHealth = _maxHealth;
        }
    }

    /// <summary>
    /// Starts player damage immune timer
    /// </summary>
    /// <param name="damageImmuneDuration">damage immune dureation in seconds</param>
    private void DamageImmuneTimer(float damageImmuneDuration)
    {
        if (!_isDamagable)
        {
            _curImmuneTimer += Time.deltaTime;
            if (_curImmuneTimer >= damageImmuneDuration)
            {
                _curImmuneTimer = 0;
                _isDamagable = true;
            }
        }
    }
    #endregion

    #region COLLISION Handler
    private void OnTriggerEnter(Collider other)
    {
        AI_Agent enemy = other.gameObject.GetComponent<AI_Agent>();
        if (enemy)
        {
            GetDamage(enemy.Damage);
        }
    }
    #endregion
}
