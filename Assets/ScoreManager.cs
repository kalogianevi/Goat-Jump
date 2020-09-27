using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{  
    public GameObject player;

    private Rigidbody2D rd2d;

    private int highScore;
    private int score;

    private bool isNewHighScore;
    
    public Text ScoreText;
    public Text HighScoreText;
    public Text NewHighScoreText;
    
    public Text ResultsScoreText;
    public Text ResultsHighScoreText;
    
    public float timeHightScore = 0.3f;
    public GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        isNewHighScore = false;
        NewHighScoreText.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(false);
        HighScoreText.gameObject.SetActive(false);
        score = 0;
        highScore = 0;
        rd2d = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
        if (_gameManager.IsGameRunning)
        {
            if (NewHighScoreText.gameObject.activeSelf)
            {
                timeHightScore -= Time.deltaTime;
                if (timeHightScore < 0)
                {
                    NewHighScoreText.gameObject.SetActive(false);
                }
            }

            var newPosition = Mathf.RoundToInt(rd2d.position.y);
            if (newPosition > 0 && newPosition > score)
            {
                HighScoreText.gameObject.SetActive(true);
                ScoreText.gameObject.SetActive(true);
                score = newPosition;
            }

            if (score > highScore)
            {
                highScore = score;
                if (!isNewHighScore)
                {
                    isNewHighScore = true;
                    NewHighScoreText.gameObject.SetActive(true);
                }
            }

            ScoreText.text = "Score : " + score.ToString();
            HighScoreText.text = "High Score : " + highScore.ToString();
        }
        
        if(_gameManager.waitToStart)
        {
            score = 0;
            isNewHighScore = false;
            timeHightScore = 0.5f;
            NewHighScoreText.gameObject.SetActive(false);
        }

        if (!_gameManager.waitToStart && !_gameManager.IsGameRunning)
        {
            ShowResults();
        }

    }

    void ShowResults()
    {
        ResultsScoreText.text = " Your Score is " + score.ToString();
        ResultsHighScoreText.text = "High Score : " + highScore.ToString();
        if (isNewHighScore)
        {
            NewHighScoreText.gameObject.SetActive(true);
        }
    }
}
