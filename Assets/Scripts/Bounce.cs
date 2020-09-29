using UnityEngine;

namespace Assets.Scripts
{
    public class Bounce : MonoBehaviour
    {
        public float BounceForce = 600.0f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //If you are the Player and you are dropping onto the platform
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 + Mathf.Epsilon)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * BounceForce);
            }
        }
    }
}
