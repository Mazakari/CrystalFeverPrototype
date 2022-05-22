// Roman Baranov 21.05.2022

using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region VARIABLES
    public static PlayerHealth Instance;

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

    /// <summary>
    /// Distance to closest enemy
    /// </summary>
    public float ClosestEnemyDistance { get; private set; } = 0f;

    /// <summary>
    /// Distance to closest crystal
    /// </summary>
    public float ClosestCrystalDistance { get; private set; } = 0f;
    #endregion

    #region UNITY Methods
    private void Start()
    {
        if (Instance)
        {
            Instance = null;
        }

        Instance = this;

        CurHealth = _maxHealth;
        UiEvents.OnPlayerHpChange.Invoke(CurHealth);

        // Health state change callbacks
        GameplayEvents.OnPlayerGetHeal.AddListener(GetHealing);
    }

    private void Update()
    {
        ClosestEnemyDistance = ClosestDistance(EnemyPool.Instance.EnemiesPool);
        UiEvents.OnClosestEnemyDistanceChange.Invoke(ClosestEnemyDistance);

        ClosestCrystalDistance = ClosestDistance(CrystalPool.Instance.CrystalsPool);
        UiEvents.OnClosestCrystalDistanceChange.Invoke(ClosestCrystalDistance);

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
            UiEvents.OnPlayerHpChange.Invoke(CurHealth);

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
        UiEvents.OnPlayerHpChange.Invoke(CurHealth);

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

    /// <summary>
    /// Find closest object distance
    /// </summary>
    /// <param name="collection">Objects collection to find closest distance</param>
    /// <returns>Distance to closest object</returns>
    private float ClosestDistance<T>(List<T> collection) where T : MonoBehaviour
    {
        float minDist = 200f;
        float dist = 0f;

        for (int i = 0; i < collection.Count; i++)
        {
            if (!collection[i].gameObject.activeSelf)
            {
                continue;
            }

            dist = Vector3.Distance(gameObject.transform.position, collection[i].transform.position);
            if (dist < minDist)
            {
                minDist = dist;
            }
        }

        return minDist;
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
