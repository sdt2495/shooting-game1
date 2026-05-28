using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        int lastScore = PlayerPrefs.GetInt("lastScore", 0);
        scoreText.text = "Score: " + lastScore;
    }
}
