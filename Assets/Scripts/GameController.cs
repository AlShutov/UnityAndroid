using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    public GameObject[] hazards;
    public GameObject powerUp;
    public GameObject powerUpSFX;
    public GameObject lifeUpSFX;
    public GameObject restartButton;


    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text gameOverText;
    public UnityEngine.UI.Text restartText;
    public UnityEngine.UI.Text livesText;
    public int maxChance;
    public int livesCount;
    public int liveScore;
    public int score;
    public int scoreDifficultyRaiser;
    public int difficultyRaised;
    public float speedIncrement;
    private int count;
    private bool gameOver;
    private bool restart;
    void Start()
    {
        speedIncrement = 0; 
        difficultyRaised = 1;
        count = 1;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        restartButton.SetActive(false);
        livesText.text = "Lives: " + livesCount;
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {
        livesText.text = "Lives: " + livesCount;
     //   if (restart)
       // {
         //   if (Input.GetKeyDown(KeyCode.R))
           // {
             //   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}
        //}  
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true) {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.Euler(90, 180, 0);


                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartButton.SetActive(true);
                restartText.text = "Continue?";
                restart = true;
                break;
            }
        }
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if (score >= count * liveScore)
        {
            livesCount = livesCount + 1;
            count = count + 1;
            Instantiate(lifeUpSFX);
        }
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

