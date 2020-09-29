using UnityEngine;

namespace Assets.Scripts
{
    public class LevelHandler : MonoBehaviour
    {
        public GameObject PlatformPrefab;
        public GameObject ExtraBouncePlatPrefab;
        public GameObject Player;

        public float Width;
        public float Height;

        private GameObject mPlat;

        void Start()
        {
            Width = 4;
            Height = 14; 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Vector2 newPlatformPosition = new Vector2(Random.Range(-Width, Width),
                Player.transform.position.y + Height + Random.Range(0.5f, 1f));
            if (collision.gameObject.name.Contains("Standard"))
            {
                // probability of a double jump platform
                if (Random.Range(1, 10) == 1)
                {
                    Destroy(collision.gameObject);
                    mPlat = (GameObject)Instantiate(ExtraBouncePlatPrefab, newPlatformPosition, Quaternion.identity);
                }
                else
                {
                    collision.gameObject.transform.position = newPlatformPosition;
                }
            }
            else
            {
                // probability of a double jump platform
                if (Random.Range(1, 10) == 1)
                {
                    collision.gameObject.transform.position = newPlatformPosition;
                }
                else
                {
                    Destroy(collision.gameObject);
                    mPlat = (GameObject)Instantiate(PlatformPrefab, newPlatformPosition, Quaternion.identity);

                }

            }
        }

        public void GenerateLevel(Vector2 playerStartPosition)
        {
            Instantiate(PlatformPrefab, new Vector2(Player.transform.position.x, playerStartPosition.y - 0.5f), Quaternion.identity);

            Instantiate(PlatformPrefab, new Vector2(Random.Range(-Width, Width), playerStartPosition.y + 3), Quaternion.identity);
            Instantiate(PlatformPrefab, new Vector2(Random.Range(-Width, Width), playerStartPosition.y + 6), Quaternion.identity);
            Instantiate(PlatformPrefab, new Vector2(Random.Range(-Width, Width), playerStartPosition.y + 8), Quaternion.identity);
            Instantiate(PlatformPrefab, new Vector2(Random.Range(-Width, Width), playerStartPosition.y + 12), Quaternion.identity);
            Instantiate(PlatformPrefab, new Vector2(Random.Range(-Width, Width), playerStartPosition.y + 15), Quaternion.identity);
            Instantiate(PlatformPrefab, new Vector2(Random.Range(-Width, Width), playerStartPosition.y + 16), Quaternion.identity);
            Instantiate(PlatformPrefab, new Vector2(Random.Range(-Width, Width), playerStartPosition.y + 18), Quaternion.identity);

        }

        public void CleanLevel()
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("platform");
            foreach (var platform in platforms)
            {
                Destroy(platform);
            }
        }
    }
}
