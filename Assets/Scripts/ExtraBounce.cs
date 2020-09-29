using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBounce : MonoBehaviour
{
    public float SpringBounceForce = 1000.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If you are the Player and you are dropping onto the platform
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0.0f + Mathf.Epsilon)
        {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * SpringBounceForce);
        }
    }
}
