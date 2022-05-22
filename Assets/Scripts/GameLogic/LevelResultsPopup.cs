// Roman Baranov 22.05.2022

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelResultsPopup : MonoBehaviour
{
    #region VARIABLES
    [Header("Level results popup referrence")]
    [SerializeField] private GameObject _levelResultsPopup = null;

    [SerializeField] private Button _restartButton = null;
    [SerializeField] private Button _menuButton = null;

    [SerializeField] private Text _levelScoresCounter = null;
    [SerializeField] private Text _bestScoresCounter = null;
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        GameplayEvents.OnPlayerDead.AddListener(LevelResult);

        _restartButton.onClick.AddListener(RestartLevel);
        _menuButton.onClick.AddListener(LoadMenu);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Activates level result popup
    /// </summary>
    private void LevelResult()
    {
        Time.timeScale = 0f;

        _levelScoresCounter.text = LevelManager.Instance.CurrentPlayerScores.ToString();
        _bestScoresCounter.text = LevelManager.Instance.BestPlayerScores.ToString();

        _levelResultsPopup.SetActive(true);

        // Save Best scores
        SaveGameManager.CurrentSaveData.bestScores = LevelManager.Instance.BestPlayerScores;
        SaveGameManager.SaveGame();
    }

    /// <summary>
    /// Restart current level
    /// </summary>
    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    /// <summary>
    /// Load main menu scene
    /// </summary>
    private void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Initial popup setup on scene start
    /// </summary>
    private void InitPopup()
    {
        #region EDITOR Assignment Checks
        if (!_levelResultsPopup)
        {
            Debug.LogError($"'_levelResultsPopup' popup is not assigned in {name}");
        }

        if (!_restartButton)
        {
            Debug.LogError($"'Restart' button is not assigned in {name}");
        }

        if (!_menuButton)
        {
            Debug.LogError($"'Menu' button is not assigned in {name}");
        }

        if (!_levelScoresCounter)
        {
            Debug.LogError($"'_levelScoresCounter' text parameter is not assigned in {name}");
        }

        if (!_bestScoresCounter)
        {
            Debug.LogError($"'_bestScoresCounter' text parameter is not assigned in {name}");
        }
        #endregion

        _levelResultsPopup.SetActive(false);

        
    }
    #endregion
}
