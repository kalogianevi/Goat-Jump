using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBounce : MonoBehaviour
{
    public float SpringBounceForce = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If you are the player and you are dropping onto the platform
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * SpringBounceForce);
        }
    }
}
