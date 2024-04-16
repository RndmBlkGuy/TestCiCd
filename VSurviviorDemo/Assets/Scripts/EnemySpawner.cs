using UnityEngine;
using System.Collections;
using TMPro; 


public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPos;
    public float startTimeBetweenSpawns;
    public float spawnRate;

    public float decreaseTime;
    public float minTime;

    private int currentWave = 1;
    private int scoreAtWaveStart;
    public TMP_Text waveText; // Reference to the TMP_Text component
    private int scoreToAdvanceWave = 100; // Score needed to advance to the next wave
    private const int maxWave = 10; // Maximum wave number

    void Start()
    {
        scoreAtWaveStart = GetPlayerScore(); // Initialize score at wave start
        UpdateWave();
    }

    void Update()
    {
        if (currentWave > maxWave)
        {
            // Optionally, stop spawning and perform end-game logic
            return; // Stops further execution if the maximum wave is reached
        }

        if (spawnRate <= 0)
        {
            SpawnEnemy();
            spawnRate = startTimeBetweenSpawns;
            if (startTimeBetweenSpawns > minTime)
            {
                startTimeBetweenSpawns -= decreaseTime;
            }
        }
        else
        {
            spawnRate -= Time.deltaTime;
        }

        CheckForWaveProgression();
    }

    private void SpawnEnemy()
    {
        int randEnemy = Random.Range(0, enemies.Length);
        int randPos = Random.Range(0, spawnPos.Length);
        Instantiate(enemies[randEnemy], spawnPos[randPos].position, Quaternion.identity);
    }

    private void CheckForWaveProgression()
    {
        int currentScore = GetPlayerScore();
        if (currentScore - scoreAtWaveStart >= scoreToAdvanceWave && currentWave <= maxWave)
        {
            currentWave++;
            scoreAtWaveStart = currentScore; // Update score at start of new wave

            if (currentWave <= maxWave)
            {
                UpdateWave();
            }
        }
    }

     private void UpdateWave()
    {
        // Clear existing enemies before starting a new wave
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in existingEnemies)
        {
            Destroy(enemy);
        }

        // Adjust difficulty or spawn rate as needed here

        // Reset the spawnRate to immediately start spawning in the new wave
        spawnRate = 0;

        // Update the wave number display
        StartCoroutine(UpdateWaveText($"Wave {currentWave}"));
    }

    IEnumerator UpdateWaveText(string text)
    {
        // Fade out
        yield return StartCoroutine(FadeTextTo(0.0f, 0.5f));
        // Update the text
        waveText.text = text;
        // Fade in
        yield return StartCoroutine(FadeTextTo(1.0f, 0.5f));
    }

    IEnumerator FadeTextTo(float targetOpacity, float duration)
    {
        // Capture the starting color
        Color color = waveText.color;
        // Calculate how much to change the opacity each frame
        float opacityChangePerFrame = (color.a - targetOpacity) / duration * Time.deltaTime;

        // While we're not at the target opacity...
        while (!Mathf.Approximately(color.a, targetOpacity))
        {
            // ... move the opacity toward the target opacity
            color.a = Mathf.MoveTowards(color.a, targetOpacity, Mathf.Abs(opacityChangePerFrame));
            waveText.color = color;
            yield return null; // Wait a frame and continue
        }
    }

    private int GetPlayerScore()
    {
        // Implement based on your score management system
        return FindObjectOfType<ScoreManager>().GetCurrentScore(); // Example placeholder
    }
}
