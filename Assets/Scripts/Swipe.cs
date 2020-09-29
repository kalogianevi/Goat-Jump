using UnityEngine;

namespace Assets.Scripts
{
    public class MobileInputManager
    {
        //inside class
        private Vector2 firstPressPos;
        private Vector2 secondPressPos;
        private Vector2 currentSwipe;
 
        public float Swipe(string direction)
        {
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
                    secondPressPos = new Vector2(t.position.x,t.position.y);

                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                    Debug.Log("Ended");

                    currentSwipe.Normalize();
                    if (direction == "Vertical")
                    {
                        //swipe upwards
                        if (currentSwipe.y > 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                        {
                            Debug.Log("up swipe");
                            return 0.5f;
                        }
                        //swipe down
                        if (currentSwipe.y < 0  && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                        {
                            Debug.Log("down swipe");
                            return -0.5f;
                        }
                    }

                    if(direction == "Horizontal")
                    {
                        //swipe left
                        if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            Debug.Log("left swipe : " +currentSwipe.x);
                            return -0.5f;
                        }

                        //swipe right
                        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            Debug.Log("right swipe : "  +currentSwipe.x);
                            return 0.5f;
                        }
                    }
                }
            }

            return 0;
        }
    
    }
}
