// Roman Baranov 21.05.2022

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region VARIABLES
    [Min(1)]
    [Header("Enemy spawn delay")]
    [SerializeField] private float _spawnDelay = 3f;
    private float _curDelay = 0f;
    #endregion

    #region UNITY Methods
    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.CurEnemies == 
            LevelManager.Instance.MaxEnemiesLimit)
        {
            return;
        }

        _curDelay += Time.deltaTime;
        if (_curDelay >= _spawnDelay)
        {
            SpawnEnemy();

            LevelManager.Instance.CurEnemies++;
            _curDelay = 0f;
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Activates enemy from the enemies pool
    /// </summary>
    private void SpawnEnemy()
    {
        AI_Agent enemy = EnemyPool.Instance.GetEnemy();
        if (enemy != null)
        {
            enemy.transform.position = gameObject.transform.position;
            enemy.gameObject.SetActive(true);
        }
    }
    #endregion
}
