using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public PlayerController MPlayerController;
        public LevelHandler MLevelHandler;

        public Text StartGameText;
        public GameObject EndGameScreen;

        public const float GAMEOVERSCREENTIME = 1.0f;

        private Vector2 playerStartPosition;
        private bool isWaitingToStart = true;
        private bool isGameRunning = false;

        public bool GetIsWaitingToStart ()
        {
            return isWaitingToStart;
        }

        public bool GetIsGameRunning()
        {
            return isGameRunning;
        }

        void Start()
        {
            playerStartPosition = MPlayerController.GetComponent<Rigidbody2D>().transform.position;
            isWaitingToStart = true;
            isGameRunning = false;
            ResetGame();
        }
    
        void Update()
        {
            if (!isGameRunning && isWaitingToStart)
            {
                StartGameText.gameObject.SetActive(true);
            }
        }

        public void StartGame()
        {
            isWaitingToStart = false;
            StartGameText.gameObject.SetActive(false);
            isGameRunning = true;
        }
    
        private void EndGame()
        {
            isGameRunning = false;
            EndGameScreen.SetActive(true);
            MLevelHandler.CleanLevel();
        }

        public void RestartGame()
        {
            StartCoroutine(nameof(RestartGameCo));
        }

        public IEnumerator RestartGameCo()
        {
            EndGame();
            yield return new WaitForSeconds(GAMEOVERSCREENTIME);
            isWaitingToStart = true;
            ResetGame();
        }

        private void ResetGame()
        {
            EndGameScreen.SetActive(false);
            MPlayerController.transform.position = playerStartPosition;
            MLevelHandler.GenerateLevel(playerStartPosition);
        }

    }
}
