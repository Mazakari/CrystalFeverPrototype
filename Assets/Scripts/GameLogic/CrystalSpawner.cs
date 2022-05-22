// Roman Baranov 21.05.2022

using UnityEngine;
using UnityEngine.AI;

public class CrystalSpawner : MonoBehaviour
{
    #region VARIABLES
    [Min(1)]
    [Header("Crystal max spawn delay")]
    [Tooltip("Each crystal spawn will be calculated by random between 0 and max spawn delay")]
    [SerializeField] private float _maxSpawnDelay = 3f;
    private float _rndSpawnDelay = 0f;
    private float _curDelay = 0f;

    private float _spawnRadius = 20f;
    #endregion

    #region UNITY Methods
    private void Start()
    {
        _rndSpawnDelay = Random.Range(0, _maxSpawnDelay);
        InitCrystals(LevelManager.Instance.MaxCrystalsLimit);
    }
    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.CurCrystals ==
            LevelManager.Instance.MaxCrystalsLimit)
        {
            return;
        }

        _curDelay += Time.deltaTime;
        if (_curDelay >= _rndSpawnDelay)
        {
            SpawnCrystal();

            LevelManager.Instance.CurCrystals++;

            _curDelay = 0f;
            _rndSpawnDelay = Random.Range(0, _maxSpawnDelay);
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Activates enemy from the enemies pool
    /// </summary>
    private void SpawnCrystal()
    {
        Crystal crystal = CrystalPool.Instance.GetCrystal();
        if (crystal != null)
        {
            //Get random spawn point on the nav mesh
            Vector3 spawnPos = GetRandomPoint();
            spawnPos.y = 1f;

            crystal.transform.position = spawnPos;
            crystal.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Gets random naw mesh point around the spawner
    /// </summary>
    /// <returns>Point to spawn crystal</returns>
    private Vector3 GetRandomPoint()
    {
        Vector3 point = Vector3.zero;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(Random.insideUnitSphere * _spawnRadius, out navMeshHit, _spawnRadius, NavMesh.AllAreas);

        point = navMeshHit.position;
        
        return point;
    }

    /// <summary>
    /// Initial crystals placement on level start
    /// </summary>
    private void InitCrystals(int crystalsAmount)
    {
        for (int i = 0; i < crystalsAmount; i++)
        {
            SpawnCrystal();

            LevelManager.Instance.CurCrystals++;
        }
    }
    #endregion
}
