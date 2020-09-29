using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public GameManager MGameManager;
        public MobileInputManager MMobileInputManager;
        public const float FALLINGTIMEBEFOREDEAD = 0.5f;
        public const float PLAYERSPEED = 10.0f;

        private Rigidbody2D rd2d;
        private float moveInput;
        private float speed;
        private float targetTime;

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
            if (MGameManager.GetIsGameRunning())
            {
                rd2d.gravityScale = 5.0f;
            
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    moveInput = MMobileInputManager.Swipe("Horizontal");

                    if (moveInput < 0 && transform.position.x > -1.5f)
                    {
                        rd2d.transform.position = new Vector2(transform.position.x - 1.75f, transform.position.y);
                    }

                    if ((moveInput > 0 && transform.position.x < 1.5f))
                    {
                        rd2d.transform.position = new Vector2(transform.position.x - 1.75f, transform.position.y);
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
                    Debug.Log(" velocity : "+ rd2d.velocity);       
                }

                if (IsFallingToDeath())
                {
                    MGameManager.RestartGame();
                }
          
                if(!IsFalling())
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
    }
}
