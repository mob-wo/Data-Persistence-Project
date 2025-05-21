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
// -play button on click load the game scene
// -exit button
//

public class MenuUI : MonoBehaviour
{

    // UI elements
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
    
        // Add button listeners
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

 
    private void OnPlayButtonClicked()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    private void OnExitButtonClicked()
    {
        // Exit the game
        // If running in the editor, stop playing

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
