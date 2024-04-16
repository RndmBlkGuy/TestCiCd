using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the TextMeshProUGUI component
    private int score = 0; // Initialize score to 0

    public static ScoreManager instance;

    private void Awake() {
        if (instance == null) {
        instance = this;
        //DontDestroyOnLoad(gameObject); // Optionally keep the score manager across scenes
    } else if (instance != this) {
        Destroy(gameObject);
    }
    }

    void Start()
    {
        score = GetScore();
        UpdateScoreText(); // Update score text on game start
    }

    // Call this method to increase score by a specific amount
    public void AddScore(int amount)
    {
        UpdateScore(amount);
        UpdateScoreText(); // Update the score text
       
    }

    // Updates the TextMeshPro text to show the current score
    void UpdateScoreText()
    {
        if (scoreText != null) // Check if the scoreText reference is set
        {
            scoreText.text = "Score: " + score; // Set the text to display the current score
        }
    }

    void UpdateScore(int newScore)
    {
        PlayerData data = DataManager.LoadPlayerData();
        data.playerScore += newScore; // Update the score
        score = data.playerScore;
        Debug.Log("New Score for Player" + data.playerScore);
        DataManager.SavePlayerData(data); // Save the updated data

    }

    public int GetScore()
    {
        return DataManager.LoadPlayerData().playerScore;
    }
    public int GetCurrentScore()
    {
        return score;
    }

}
