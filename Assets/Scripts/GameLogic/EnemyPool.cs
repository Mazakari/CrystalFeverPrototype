// Roman Baranov 21.05.2022

using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    #region VARIABLES
    public static EnemyPool Instance = null;

    [Header("Enemy Pool Settings")]
    [Min(1)]
    [SerializeField] private int _poolSize = 10;

    [SerializeField] private AI_Agent _enemyPrefab = null;
    /// <summary>
    /// Enemies pool
    /// </summary>
    public List<AI_Agent> EnemiesPool { get; private set; } = new List<AI_Agent> ();
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        if (Instance)
        {
            Instance = null;
        }

        Instance = this;

        CreateEnemiesPool();

        GameplayEvents.OnEnemyDead.AddListener(ResetEnemy);
    }
    #endregion

    #region PUBLIC Methods
    public AI_Agent GetEnemy()
    {
        // Get inactive enemy from the pool
        for (int i = 0; i < EnemiesPool.Count; i++)
        {
            if (!EnemiesPool[i].gameObject.activeSelf)
            {
                return EnemiesPool[i];
            }
        }

        CreateEnemiesPool();
        GetEnemy();

        return null;
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Reset enemy position and return it in the pool
    /// </summary>
    /// <param name="enemy">Enemy to reset</param>
    private void ResetEnemy(AI_Agent enemy)
    {
        // Deactivate enemy object
        enemy.gameObject.SetActive(false);
        //enemy.RB.velocity = Vector3.zero;

        // Reset enemy position
        enemy.transform.position = transform.position;
        enemy.transform.SetParent(transform);
    }

    /// <summary>
    /// Spawn enemies pool
    /// </summary>
    private void CreateEnemiesPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            AI_Agent enemy = Instantiate(_enemyPrefab, gameObject.transform);
            enemy.gameObject.SetActive(false);

            EnemiesPool.Add(enemy);
        }
    }
    #endregion
} 

