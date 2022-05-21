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
    private int _curHealth = 3;

    private bool _isDamagable = true;
    [SerializeField] private float _damageImmuneDuration = 3f;
    private float _curImmuneTimer = 0f;
    #endregion

    #region UNITY Methods
    private void Start()
    {
        _curHealth = _maxHealth;

        // Health state change callbacks
        GameplayEvents.OnPlayerGetDamage.AddListener(GetDamage);
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
    /// <param name="value">Damage amount</param>
    private void GetDamage(int value)
    {
        if (_isDamagable)
        {
            _curHealth -= value;
            _isDamagable = false;
            if (_curHealth <= 0)
            {
                _curHealth = 0;
               
                // Send player death callback
                GameplayEvents.OnPlayerDead.Invoke();

                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Increase heath by given amount
    /// </summary>
    /// <param name="value">Healing amount</param>
    private void GetHealing(int value)
    {
        _curHealth += value;
        if (_curHealth> _maxHealth)
        {
            _curHealth = _maxHealth;
        }
    }
    #endregion

    #region PRIVATE Methods
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

    private void OnTriggerEnter(Collider other)
    {
        AI_Agent enemy = other.gameObject.GetComponent<AI_Agent>();
        if (enemy)
        {
            GetDamage(enemy.Damage);
            return;
        }

        Crystal crystal = other.GetComponent<Crystal>();
        if (crystal)
        {
            if (_curHealth >= _maxHealth)
            {
                return;
            }

            GetHealing(crystal.HealAmount);
            crystal.Pickup();
        }
    }
    #endregion
}
