using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] string BestScoreString = "HighScore";

    [SerializeField] float GameEndSeconds = 8f;

    int Score = 0;

    void Start()
    {
        highScoreText.text = "BEST: " + PlayerPrefs.GetInt(BestScoreString, 0);

        InvokeRepeating("AddScore", 1f, 1f);
        Invoke("HandleBestScore", GameEndSeconds);
    }

    void AddScore()
    {
        Score += 10;
        scoreText.text = "SCORE: " + Score;
    }

    void HandleBestScore()
    {
        int bestScore = PlayerPrefs.GetInt(BestScoreString, 0);

        if (Score > bestScore)
        {
            PlayerPrefs.SetInt(BestScoreString, Score);
            highScoreText.text = "BEST: " + Score;
        }

        //highScoreText.text = "BEST: " + Score;

        Time.timeScale = 0f;
    }

}
