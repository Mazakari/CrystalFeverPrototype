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

    /// <summary>
    /// Scores collected during active game session
    /// </summary>
    public int CurrentPlayerScores { get; private set; } = 0;

    /// <summary>
    /// Best player scores among all gameplay sessions
    /// </summary>
    public int BestPlayerScores { get; private set; } = 0;
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
        GameplayEvents.OnCrystalPickup.AddListener(CrystalPickup);
        GameplayEvents.OnCrystalDestroyed.AddListener(CrystalDestroyed);
        GameplayEvents.OnEnemyDead.AddListener(EnemyLimitUpdate);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Decrease spawned crystals limit
    /// </summary>
    private void CrystalDestroyed(Crystal crystal)
    {
        CurCrystals--;
    }

    /// <summary>
    /// Decrease spawned crystals limit
    /// </summary>
    private void CrystalPickup(Crystal crystal)
    {
        // Add scores
        CurrentPlayerScores++;

        //Check best player scores
        if (CurrentPlayerScores >= BestPlayerScores)
        {
            BestPlayerScores = CurrentPlayerScores;
        }

        CurCrystals--;
    }

    /// <summary>
    /// Decrease spawned enemies limit
    /// </summary>
    private void EnemyLimitUpdate(AI_Agent enemy)
    {
        CurEnemies--;
    }
    #endregion

}
