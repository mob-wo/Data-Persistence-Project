using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// main menu UI
// This script is responsible for the main menu UI
// It will handle the button clicks and other UI elements
// It will also handle the transitions between the main menu and the game
// #ui (serialized)
// -High score load at System manager
// -name input
// -play button on click load the game scene
// -exit button
//

public class MenuUI : MonoBehaviour
{

    // UI elements
    [SerializeField] private Text highScoreText;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;



    private void Start()
    {
        // Load high score and update UI
        UpdateHighScoreText();

        // Add button listeners
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void UpdateHighScoreText()
    {
        var highScore = SystemManager.Instance.GetHighScore();
        highScoreText.text = "High Score: " + highScore.playerName + " : " + highScore.score;
    }

    private void OnPlayButtonClicked()
    {
        // Check if player name is empty
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            SystemManager.Instance.CurrentPlayerName = "NoNamePlayer";
        } else
        {
            SystemManager.Instance.CurrentPlayerName = playerNameInput.text;
        }
        Debug.Log("Player Name: " + SystemManager.Instance.CurrentPlayerName);
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    private void OnExitButtonClicked()
    {
        // Exit the game
        // If running in the editor, stop playing
        // save settings
        SystemManager.Instance.SaveSettings();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
