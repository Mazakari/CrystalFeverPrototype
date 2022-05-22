// Roman Baranov 22.05.2022

using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    #region VARIABLES
    [Header("Player counters")]
    [SerializeField] private Text _playerHpCounter = null;
    [SerializeField] private Text _playerScoresCounter = null;

    [Header("Enemy counters")]
    [SerializeField] private Text _enemyCounter = null;
    [SerializeField] private Text _closestEnemyCounter = null;

    [Header("Crystal counters")]
    [SerializeField] private Text _crystalCounter = null;
    [SerializeField] private Text _closestCrystalCounter = null;
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        UiEvents.OnPlayerHpChange.AddListener(UpdatePlayerHpCounter);
        UiEvents.OnPlayerScoreChange.AddListener(UpdatePlayerScoresCounter);

        UiEvents.OnEnemiesCountChange.AddListener(UpdateEnemiesCounter);
        UiEvents.OnClosestEnemyDistanceChange.AddListener(UpdateClosestEnemyCounter);

        UiEvents.OnCrystalsCountChange.AddListener(UpdateCrystalsCounter);
        UiEvents.OnClosestCrystalDistanceChange.AddListener(UpdateClosestCrystalCounter);
    }
    #endregion

    #region CALLBACK Handlers
    /// <summary>
    /// Updates player HP counter
    /// </summary>
    /// <param name="newHpValue">New player HP value</param>
    private void UpdatePlayerHpCounter(int newHpValue)
    {
        _playerHpCounter.text = newHpValue.ToString();
    }

    /// <summary>
    /// Updates player scores counter
    /// </summary>
    /// <param name="newScoresValue">New scores value</param>
    private void UpdatePlayerScoresCounter(int newScoresValue)
    {
        _playerScoresCounter.text = newScoresValue.ToString();
    }

    /// <summary>
    /// Updates crystals counter
    /// </summary>
    /// <param name="newCrystalsValue">New crystals value</param>
    private void UpdateCrystalsCounter(int newCrystalsValue)
    {
        _crystalCounter.text = newCrystalsValue.ToString();
    }

    /// <summary>
    /// Updates enemies counter
    /// </summary>
    /// <param name="newEnemiesValue">New crystals value</param>
    private void UpdateEnemiesCounter(int newEnemiesValue)
    {
        _enemyCounter.text = newEnemiesValue.ToString();
    }

    /// <summary>
    /// Updates closest enemy distance counter
    /// </summary>
    /// <param name="newDistanceValue">New distance value</param>
    private void UpdateClosestEnemyCounter(float newDistanceValue)
    {
        if (newDistanceValue >= 200f)
        {
            _closestEnemyCounter.text = "0";
            return;
        }

        _closestEnemyCounter.text = newDistanceValue.ToString("F1");
    }

    /// <summary>
    /// Updates closest crystal distance counter
    /// </summary>
    /// <param name="newDistanceValue">New distance value</param>
    private void UpdateClosestCrystalCounter(float newDistanceValue)
    {
        _closestCrystalCounter.text = newDistanceValue.ToString("F1");
    }
    #endregion
}
