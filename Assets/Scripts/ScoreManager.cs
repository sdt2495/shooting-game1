using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public Text scoreText;

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    // Åö GameOver éûÇ…ÉXÉRÉAÇï€ë∂Ç∑ÇÈ
    public void SaveScore()
    {
        PlayerPrefs.SetInt("lastScore", score);
    }
}
