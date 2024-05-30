using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float startWait;
    public int hazardCount;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public float spawnWait;
    public float waveWait;
    public Text restartText;
    public Text gameOverText;
    public Text scoreText;
    public int score;

    private bool gameOver;
    private bool restart;

    void Start()
    {
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnValues.x, spawnValues.x),
                    spawnValues.y,
                    spawnValues.z
                );
                Quaternion spawnRotation = Quaternion.identity;

                GameObject instantiatedHazard = Instantiate(hazard, spawnPosition, spawnRotation);
                Debug.Log($"Spawned {instantiatedHazard.name} at {spawnPosition}");
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            // Restart the game or reload the scene
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        Debug.Log("Score updated: " + score);
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        Debug.Log("Game Over triggered");
        gameOver = true;
    }
}