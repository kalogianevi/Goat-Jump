using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour
    {  
        public GameObject Player;
        public GameManager MGameManager;

        public Text ScoreText;
        public Text HighScoreText;
        public Text NewHighScoreText;
        public Text ResultsScoreText;
        public Text ResultsHighScoreText;
    
        public float TimeHighScoreAppears = 0.3f;

        private Rigidbody2D rd2d;
        private int highScore;
        private int score;
        private bool isNewHighScore;

        void Start()
        {
            isNewHighScore = false;
            NewHighScoreText.gameObject.SetActive(false);
            ScoreText.gameObject.SetActive(false);
            HighScoreText.gameObject.SetActive(false);
            score = 0;
            highScore = 0;
            rd2d = Player.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
     
            if (MGameManager.GetIsGameRunning())
            {
                if (NewHighScoreText.gameObject.activeSelf)
                {
                    TimeHighScoreAppears -= Time.deltaTime;
                    if (TimeHighScoreAppears < 0)
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
        
            if(MGameManager.GetIsWaitingToStart())
            {
                score = 0;
                isNewHighScore = false;
                TimeHighScoreAppears = 0.5f;
                NewHighScoreText.gameObject.SetActive(false);
            }

            if (!MGameManager.GetIsWaitingToStart() && !MGameManager.GetIsGameRunning())
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
}
