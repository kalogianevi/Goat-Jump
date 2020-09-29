using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public GameManager MGameManager;
        public MobileInputManager MMobileInputManager;
        public const float FALLINGTIMEBEFOREDEAD = 0.5f;
        public const float PLAYERSPEED = 10.0f;
        public const float MOVEDURATION = 0.025f;
        private Rigidbody2D rd2d;
        private float moveInput;
        private float speed;
        private float targetTime;
        
        private Vector2 startVelocity;
        private Vector2 endVelocity;
        void Start()
        {
            MMobileInputManager = new MobileInputManager();
            rd2d = GetComponent<Rigidbody2D>();

            targetTime = FALLINGTIMEBEFOREDEAD;
            speed = PLAYERSPEED;
            Time.timeScale = 1;
            rd2d.gravityScale = 0.0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (MGameManager.GetIsGameRunning() && !MGameManager.GetIsWaitingToStart())
            {
                rd2d.gravityScale = 5.0f;

                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    moveInput = MMobileInputManager.Swipe("Horizontal");

                    if (moveInput < 0 || moveInput > 0 )
                    {
                        StartCoroutine(MobileMove(moveInput));
                    }
        
                }
                else
                {
                    moveInput = Input.GetAxis("Horizontal");
                    rd2d.velocity = new Vector2(moveInput * speed, rd2d.velocity.y);

                }

                TurnPlayer(moveInput);

                if (moveInput != 0)
                {
                    Debug.Log(" moveInput : " + moveInput);
                    Debug.Log(" velocity : " + rd2d.velocity);
                }

                if (IsFallingToDeath())
                {
                    MGameManager.RestartGame();
                }

                if (!IsFalling())
                {
                    targetTime = FALLINGTIMEBEFOREDEAD;
                }

            }
            else
            {
                rd2d.gravityScale = 0.0f;
                rd2d.velocity = new Vector2(0.0f, 0.0f);

                if (MGameManager.GetIsWaitingToStart())
                {
                    if (Input.touchCount > 0 || Input.GetMouseButton(0))
                    {
                        MGameManager.StartGame();
                    }
                }

            }
        }

        bool IsFalling()
        {
            var direction = transform.InverseTransformDirection(rd2d.velocity);
            if (direction.y < 0)
            {
                return true;
            }

            return false;
        }

        bool IsFallingToDeath()
        {
            if (IsFalling())
            {
                targetTime -= Time.deltaTime;
                if (targetTime <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        void TurnPlayer(float moveInput)
        {
            if (moveInput < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        IEnumerator MobileMove(float moveInput)
        {
            float moveTime = 0;
            startVelocity = rd2d.velocity;
            endVelocity = new Vector2(moveInput * speed, rd2d.velocity.y);

            while (moveTime < MOVEDURATION)
            {
                moveTime += Time.deltaTime;
                rd2d.velocity = Vector3.Lerp(startVelocity, endVelocity, moveTime /  MOVEDURATION);
                yield return null;
            }
 
        }
    }
}
