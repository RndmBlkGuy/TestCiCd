using UnityEngine;
using UnityEngine.SceneManagement; // Include this for scene management
using TMPro;


public class GameManager : MonoBehaviour
{

    public ScoreManager scoreM;
    public int scoreValue;
    public bool isPaused;
    // Singleton instance
    public static GameManager Instance { get; private set; }

    public GameObject gameOverScreen;
    public TMP_Text finalScoreText;
    public TMP_Text scoreText;

    // Enum to represent game states
    public enum GameState
    {
        Play,
        Pause,
        GameOver,
        MainMenu,
        CharacterMenu
    }

    // Current game state
    public GameState CurrentState { get; private set; }

    private void Awake()
{
    // Singleton pattern setup (ensure only one instance exists)
    if (Instance == null)
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject); // Keep the manager across scenes
    }
    else
    {
        Destroy(gameObject);
        return;
    }

    // Determine and set the initial game state based on the current scene
    DetermineInitialState();
}
private void Update()
    {
        // Example of toggling pause state with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Play)
            {
                ChangeGameState(GameState.Pause);
            }
            else if (CurrentState == GameState.Pause)
            {
                ChangeGameState(GameState.Play);
            }
        }
    }


    // Method to change the game state
    public void ChangeGameState(GameState newState)
    {
        CurrentState = newState;

        // Optional: Perform actions based on the new state
        HandleStateChange(newState);
    }

    // Handle actions on changing states
    private void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Play:
                Time.timeScale = 1.0f; // Resume game time
                ShowPauseScreen(false);
                break;
            case GameState.Pause:
                Time.timeScale = 0f; // Pause game time
                ShowPauseScreen(true);
                break;
            case GameState.GameOver:
                Time.timeScale = 0f; // Pause game time
                ShowGameOverScreen();
                break;
            case GameState.MainMenu:
                // Logic for main menu
                break;
            case GameState.CharacterMenu:
            Time.timeScale = 1.0f; // Resume game time
                // Logic for character menu
                break;
        }
    }

    public void ShowPauseScreen(bool show)
    {
        // Toggle pause screen visibility
        isPaused = show;
        // Assuming you have a canvas or panel for the pause menu
        // pauseMenuPanel.SetActive(show);
    }

    public void ShowGameOverScreen()
    {
        // Display the game over screen
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Score: " + scoreM.GetCurrentScore();
    }

    public void GameOver(){
        ChangeGameState(GameState.GameOver);
    }
    public void LoadCharacterMenu()
    {
        HandleStateChange(GameState.CharacterMenu);
        SceneManager.LoadScene("CharacterMenu");
    }

    public void LoadLevel()
    {
        HandleStateChange(GameState.Play);
        SceneManager.LoadScene("Gameplay");
    }

    // Determine initial game state (you can expand this method based on your game's logic)
    private void DetermineInitialState()
    {
    // Get the current scene name
    string sceneName = SceneManager.GetActiveScene().name;

    // Set the game state based on the scene name
    if (sceneName == "MainMenu")
    {
        ChangeGameState(GameState.MainMenu);
    }
    else if (sceneName == "CharacterMenu")
    {
        ChangeGameState(GameState.CharacterMenu);
    }
    else if (sceneName == "Gameplay")
    {
        ChangeGameState(GameState.Play);
    }
    // Add more conditions as needed for other scenes/game states
    else
    {
        Debug.LogWarning("Scene name does not match any predefined game states.");
        // Optionally, set a default state
        ChangeGameState(GameState.MainMenu); // Set to whatever default state makes sense for your game
    }
}

}
