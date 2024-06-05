using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text highScoreText; // New Text for displaying high score
    public Text restartText; 
    public Text gameOverText;  
    public Button shopButton1; // Button for the first shop item
    public Button shopButton2; // Button for the second shop item
    public Button shopButton3; // Button for the third shop item

    private int score;
    private int highScore; // New variable to store high score
    private bool gameOver;
    private bool restart;

    void Start ()
    {
        gameOver = false;
        restart = false;

        restartText.text = "";
        gameOverText.text = "";

        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Load high score from PlayerPrefs
        UpdateScore();
        UpdateHighScore(); // Update high score text

        shopButton1.onClick.AddListener(BuyExtraLife);
        shopButton2.onClick.AddListener(BuyDoubleSpeed);
        shopButton3.onClick.AddListener(BuyTripleShoot);

        StartCoroutine (SpawnWaves ());
    }

    void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneBuildIndex);
            }
        }
    }

    IEnumerator SpawnWaves ()
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

                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                // Update high score if the current score is greater than the saved high score
                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt("HighScore", highScore); // Save high score to PlayerPrefs
                    UpdateHighScore(); // Update high score text
                }
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateHighScore()
    {
        highScoreText.text = "High Score: " + highScore;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    void BuyExtraLife()
    {
        if (score >= 100)
        {
            score -= 100;
            // Add extra life functionality here
            UpdateScore();
        }
    }

    void BuyDoubleSpeed()
    {
        if (score >= 200)
        {
            score -= 200;
            // Add double speed functionality here
            UpdateScore();
        }
    }

    void BuyTripleShoot()
    {
        if (score >= 300)
        {
            score -= 300;
            // Add triple shoot functionality here
            UpdateScore();
        }
    }
}


