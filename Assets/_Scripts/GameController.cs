using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText, maxScoreText;
    public Text gameOverText, timeText, maxTimeSaveText;

    private bool gameOver;
    private bool restart;
    private int score, maxScore;

    public GameObject restartButton;

    float currentTimeChronometer, savecurrentTimeChronometer;

    // Start is called before the first frame update
    void Start()
    {
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        savecurrentTimeChronometer = PlayerPrefs.GetFloat("Time", 0f);

        gameOver = false;
        restart = false;

        restartButton.SetActive(false);
        maxTimeSaveText.enabled = false;
        maxScoreText.enabled = false;

        gameOverText.text = "";

        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            // Boton
            restartButton.SetActive(true);
            maxTimeSaveText.enabled = true;
            maxScoreText.enabled = true;
            
            UpdateScore();
        }


        if (!gameOver)
        {
            currentTimeChronometer += Time.deltaTime;
            timeText.text = "Time: " + currentTimeChronometer.ToString("#");
        }

        // string mins = ((int)currentTimeChronometer / 60).ToString("00");
        // int min = (int)currentTimeChronometer % 60;
        // string segs = min.ToString("00");

        // string timerString = string.Format("{00}:{01}", mins, segs);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {

                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver == true)
            {
                restart = true;
                Debug.Log("Restart");
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);

            savecurrentTimeChronometer = currentTimeChronometer;
            PlayerPrefs.SetFloat("Time", savecurrentTimeChronometer);
        }
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        maxScoreText.text = "Max Score: " + maxScore;

        maxTimeSaveText.text = "Time: " + savecurrentTimeChronometer.ToString("#");
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        Debug.Log("Gameover");
    }
}
