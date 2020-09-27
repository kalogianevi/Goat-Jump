using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject doubleBouncePlatPrefab;
    public GameObject player;
    private GameObject _myPlat;

   
    // Start is called before the first frame update
    void Start()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // probability of a double jump platform
        if (Random.Range(1, 7) > 1)
        {
            _myPlat = (GameObject) Instantiate(platformPrefab, new Vector2(Random.Range(-5.5f, 5.5f), player.transform.position.y + (14 + Random.Range(0.5f, 1f))), Quaternion.identity);
    
        }
        else
        {
            _myPlat = (GameObject) Instantiate(doubleBouncePlatPrefab, new Vector2(Random.Range(-4.5f, 4.5f), player.transform.position.y + (14 + Random.Range(0.5f, 1f))), Quaternion.identity);
        }
        
        Destroy(collision.gameObject);
        
    }
}
