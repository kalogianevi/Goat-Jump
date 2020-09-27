using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class MobileInputManager
{
    //inside class
   private Vector2 firstPressPos;
   private Vector2 secondPressPos;
    private Vector2 currentSwipe;
 
    public float Swipe(string direction)
    {
        var touch = 0.0f;
        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x,t.position.y);
            }
            if(t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x,t.position.y);
                           
                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                Debug.Log("Ended");
                
                //normalize the 2d vector
                currentSwipe.Normalize();
                if (direction == "Vertical")
                {
                    //swipe upwards
                    if (currentSwipe.y > 0 &&  currentSwipe.x > -0.1f && currentSwipe.x < 0.1f)
                    {
                        Debug.Log("up swipe");
                        return 1;
                    }
                    //swipe down
                    if (currentSwipe.y < 0  && currentSwipe.x > -0.1f && currentSwipe.x < 0.1f)
                    {
                        Debug.Log("down swipe");
                        return -1;
                    }
                }

                if(direction == "Horizontal")
                {
                    //swipe left
                    if (currentSwipe.x < 0)// && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        Debug.Log("left swipe : " +currentSwipe.x);
                        currentSwipe.x = Mathf.Clamp(currentSwipe.x,  -1f, -0.5f);
                        return -1;
                    }

                    //swipe right
                    if (currentSwipe.x > 0 )//&& currentSwipe.y > -0.1f && currentSwipe.y < 0.1f)
                    {
                        Debug.Log("right swipe : "  +currentSwipe.x);
                        currentSwipe.x = Mathf.Clamp(currentSwipe.x, 0.5f, 1.0f);
                        return 1;
                    }
                }
            }
        }

        return 0;
    }
    
}
