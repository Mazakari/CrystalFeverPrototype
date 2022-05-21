//Roman Baranov 21.05.2022

using System.Collections.Generic;
using UnityEngine;

public class CrystalPool : MonoBehaviour
{
    #region VARIABLES
    public static CrystalPool Instance = null;

    [Header("Crystal Pool Settings")]
    [Min(1)]
    [SerializeField] private int _poolSize = 10;

    [SerializeField] private Crystal _crystalPrefab = null;
    /// <summary>
    /// Crystal pool
    /// </summary>
    public List<Crystal> CrystalsPool { get; private set; } = new List<Crystal>();
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

        CreateCrystalsPool();

        GameplayEvents.OnCrystalPickup.AddListener(ResetCrystal);
    }
    #endregion

    #region PUBLIC Methods
    public Crystal GetCrystal()
    {
        // Get inactive crystal from the pool
        for (int i = 0; i < CrystalsPool.Count; i++)
        {
            if (!CrystalsPool[i].gameObject.activeSelf)
            {
                return CrystalsPool[i];
            }
        }

        CreateCrystalsPool();
        GetCrystal();

        return null;
    }

    /// <summary>
    /// Reset crystal position and return it in the pool
    /// </summary>
    /// <param name="crystal">Crystal to reset</param>
    public void ResetCrystal(Crystal crystal)
    {
        // Deactivate crystal object
        crystal.gameObject.SetActive(false);

        //enemy.RB.velocity = Vector3.zero;

        // Reset crystal position
        crystal.transform.position = transform.position;
        crystal.transform.SetParent(transform);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Spawn crystal pool
    /// </summary>
    private void CreateCrystalsPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Crystal crystal = Instantiate(_crystalPrefab, gameObject.transform);
            crystal.gameObject.SetActive(false);

            CrystalsPool.Add(crystal);
        }
    }
    #endregion
}
