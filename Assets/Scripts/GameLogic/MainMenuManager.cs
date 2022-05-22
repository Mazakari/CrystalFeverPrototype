// Roman Baranov 22.05.2022

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Button _playButton = null;
    #endregion

    #region UNITY Methods
    private void Start()
    {
        _playButton.onClick.AddListener(PlayGame);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Loads the first game level
    /// </summary>
    private void PlayGame()
    {
        if (!_playButton)
        {
            Debug.LogError($"'Play' button is not assigned in {name}!");
            return;
        }
       
        SceneManager.LoadScene(1);
    }
    #endregion
}
