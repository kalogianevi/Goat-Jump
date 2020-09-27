using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rd2d;
    private float moveInput;
    private float speed = 10.0f;
    private float targetTime = 1.5f;
    private bool isFalling = false;
    
    public GameManager _MyGameManager;
    public MobileInputManager _myMobileInputManager;
    void Start()
    {          
        Time.timeScale = 1;
        _myMobileInputManager = new MobileInputManager();
        rd2d = GetComponent<Rigidbody2D>();
        rd2d.gravityScale = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_MyGameManager.IsGameRunning)
        {
            rd2d.gravityScale = 5.0f;
            
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                moveInput = _myMobileInputManager.Swipe("Horizontal");

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

           
            if (moveInput != 0)
            {
                Debug.Log(" moveInput : " + moveInput);
                Debug.Log(" velocity : "+ rd2d.velocity);       
            }

            
             
          
            var direction = transform.InverseTransformDirection(rd2d.velocity);        
            if (direction.y < 0)                                                       
            {                                                                          
                isFalling = true;                                                      
            }                                                                          
            else                                                                       
            {                                                                          
                isFalling = false;                                                     
            }                                                                          
                                                                            
            if (isFalling)                                                             
            {                                                                          
                targetTime -= Time.deltaTime;                                          
                if (targetTime <= 0)                                                   
                {                                                                      
                    _MyGameManager.RestartGame();                                      
                }                                                                      
            }                                                                          
                                                                            
            if (!isFalling)                                                            
            {                                                                          
                targetTime = 1.5f;                                                     
            }
        }
        else
        { 
            rd2d.gravityScale = 0.0f;
            rd2d.velocity = new Vector2(0.0f, 0.0f);   
            if (_MyGameManager.waitToStart)
            {
                if (Input.touchCount > 0 || Input.GetMouseButton(0))     
                {
                    _MyGameManager.StartGame();
                }
            }
           
        }
    }
}
