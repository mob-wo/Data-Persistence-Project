using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// system manager
// This script is responsible for managing the game systems
// It is a singleton, so it can be accessed from anywhere in the game
// It is also responsible for initializing the game systems
// It is also responsible for updating the game systems
// #settings define at nested class
// -players
//   -player name
//   -best score
// Settings Save and Load at JSON
// #additional methods
// -SaveSettings
// -LoadSettings
// -GetHighScore returns the high score and player name
// -GetBestScore returns the best score
// -UpdateBestScore updates the best score returns true if the score is updated



public class SystemManager : MonoBehaviour
{
    // Singleton instance
    public static SystemManager Instance { get; private set; }
    // Current player name property
    public string CurrentPlayerName { get; set; }

    // Settings class
    [System.Serializable]
    public class Settings
    {
        public List<Player> players = new List<Player>();
    }

    [System.Serializable]
    public class Player
    {
        public string playerName;
        public int bestScore;
    }

    // Settings instance
    private Settings settings = new Settings();

    // File path for settings
    private string filePath;

    private void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Path.Combine(Application.persistentDataPath, "settings.json");
        LoadSettings();
    }

    private void Update()
    {
        // Update game systems here if needed
    }

    // Save settings to JSON file
    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(filePath, json);
    }

    // Load settings from JSON file
    public void LoadSettings()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            settings = JsonUtility.FromJson<Settings>(json);
        }
        else
        {
            Debug.LogWarning("Settings file not found. Creating a new one.");
            SaveSettings();
        }
    }

    // Get high score and player name
    public (string playerName, int score) GetHighScore()
    {
        string playerName = "";
        int highScore = 0;

        foreach (var player in settings.players)
        {
            if (player.bestScore > highScore)
            {
                highScore = player.bestScore;
                playerName = player.playerName;
            }
        }

        return (playerName, highScore);
    }

    // Get best score for a specific player
    public int GetBestScore(string playerName)
    {
        foreach (var player in settings.players)
        {
            if (player.playerName == playerName)
            {
                return player.bestScore;
            }
        }
        return 0;
    }

    // Update best score for a specific player
    public bool UpdateBestScore(string playerName, int score)
    {
        bool updated = false;
        // Check if player already exists
        bool playerExists = false;

        foreach (var player in settings.players)
        {
            if (player.playerName == playerName)
            {
                playerExists = true;
                if (score > player.bestScore)
                {
                    player.bestScore = score;
                    updated = true;
                }
                break;
            }
        }
        // If player does not exist, add them
        if (!playerExists)
        {
            Player newPlayer = new Player
            {
                playerName = playerName,
                bestScore = score
            };
            settings.players.Add(newPlayer);
            updated = true;
        }
        return updated;
    }



}
