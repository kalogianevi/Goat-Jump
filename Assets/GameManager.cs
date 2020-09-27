using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    private Vector2 playerStartPosition;
    public bool waitToStart = true;

    public bool IsGameRunning = false;
    public GameObject platformPrefab;

    public Text StartGameText;
    public GameObject EndGameScreen;
    void Start()
    {
        playerStartPosition = playerController.GetComponent<Rigidbody2D>().transform.position;
        waitToStart = true;
        IsGameRunning = false;
        ResetGame();
    }
    
    void Update()
    {
        if (!IsGameRunning && waitToStart)
        {
            StartGameText.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    { 
        
        waitToStart = false;
        StartGameText.gameObject.SetActive(false);
        // playerController.gameObject.SetActive(true);
        IsGameRunning = true;
    }
    
    private void EndGame()
    {
        //playerController.gameObject.SetActive(false);
        IsGameRunning = false;
        
        EndGameScreen.SetActive(true);
        //Generator???
        GameObject[] platforms = GameObject. FindGameObjectsWithTag("platform");
        foreach (var platform in platforms)
        {
            Destroy(platform);
        }
       
    }
    public void RestartGame()
    {
        StartCoroutine(nameof(RestartGameCo));
    }

    public IEnumerator RestartGameCo()
    {
        EndGame();
        yield return new WaitForSeconds(1.0f);
        waitToStart = true;
        ResetGame();
    }

    private void ResetGame()
    {
        EndGameScreen.SetActive(false);
        GenerateLevel();
        playerController.transform.position = playerStartPosition;
    }
//make other class level generator
    private void GenerateLevel()
    {
        Instantiate(platformPrefab, new Vector2(playerStartPosition.x, playerStartPosition.y - 2), Quaternion.identity);
       
        Instantiate(platformPrefab, new Vector2(UnityEngine.Random.Range(-4.5f, 4.5f), playerStartPosition.y + 6), Quaternion.identity);
        Instantiate(platformPrefab, new Vector2(UnityEngine.Random.Range(-4.5f, 4.5f), playerStartPosition.y + 8), Quaternion.identity);
        Instantiate(platformPrefab, new Vector2(UnityEngine.Random.Range(-4.5f, 4.5f), playerStartPosition.y + 10), Quaternion.identity);
        Instantiate(platformPrefab, new Vector2(UnityEngine.Random.Range(-4.5f, 4.5f), playerStartPosition.y + 13), Quaternion.identity);
        Instantiate(platformPrefab, new Vector2(UnityEngine.Random.Range(-4.5f, 4.5f), playerStartPosition.y + 15), Quaternion.identity);
        Instantiate(platformPrefab, new Vector2(UnityEngine.Random.Range(-4.5f, 4.5f), playerStartPosition.y + 17), Quaternion.identity);
        Instantiate(platformPrefab, new Vector2(UnityEngine.Random.Range(-4.5f, 4.5f), playerStartPosition.y + 20), Quaternion.identity);
    }
}
