// Roman Baranov 21.05.2022

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region VARIABLES
    public static LevelManager Instance;

    [Min(1)]
    [Header("Maximum spawned enemies limit")]
    [SerializeField] private int _maxEnemiesLimit = 5;
    /// <summary>
    /// Max spawned enemies limit
    /// </summary>
    public int MaxEnemiesLimit { get { return _maxEnemiesLimit; } }

    /// <summary>
    /// Current active enemies count
    /// </summary>
    public int CurEnemies { get; set; } = 0;

    [Min(1)]
    [Header("Maximum spawned crystals limit")]
    [SerializeField] private int _maxCrystalsLimit = 5;
    /// <summary>
    /// Max spawned crystals limit
    /// </summary>
    public int MaxCrystalsLimit { get { return _maxCrystalsLimit; } }

    /// <summary>
    /// Current active crystals count
    /// </summary>
    public int CurCrystals { get; set; } = 0;

    [Min(1)]
    [Header("Crystals scores for pickup")]
    [SerializeField] private int _crystalPickupScores = 1;
    /// <summary>
    /// Pickup crystal scores
    /// </summary>
    public int CrystalPickupScores { get { return _crystalPickupScores; } }
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance)
        {
            Instance = null;
        }

        Instance = this;
    }

    private void Start()
    {
        GameplayEvents.OnCrystalPickup.AddListener(CrystalsLimitUpdate);
        GameplayEvents.OnEnemyDead.AddListener(EnemyLimitUpdate);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Decrease spawned crystals limit
    /// </summary>
    private void CrystalsLimitUpdate(Crystal crystal)
    {
        CurCrystals--;
    }

    /// <summary>
    /// Decrease spawned enemies limit
    /// </summary>
    private void EnemyLimitUpdate()
    {
        CurEnemies--;
    }
    #endregion

}
