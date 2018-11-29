using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeScript : MonoBehaviour
{
    public CowboyMovement cowboyMovement;

    protected bool tap, swipeUp;
    public bool Tap { get { return tap; } }
    public bool SwipeUp { get { return swipeUp; } }

    public bool fingerInContact;

    public Vector2 startTouch, SwipeDelta;
    public Vector2 swipeDelta { get { return SwipeDelta; } }

    public int screenWidth;

    public bool fingerMoved;

    private void Start()
    {
        screenWidth = Screen.height / 30;
    }

    private void Update()
    {
        swipeUp = tap = false;

        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
            fingerInContact = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!fingerMoved && fingerInContact)
            {
                print("tap ");
                tap = true;
                cowboyMovement.ChangeDirection();

                Reset();

            }

            fingerMoved = false;
            fingerInContact = false;
        }

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = Input.touches[0].position;
                fingerInContact = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (!fingerMoved && fingerInContact)
                {
                    print("tap ");
                    tap = true;
                    cowboyMovement.ChangeDirection();

                    Reset();

                }

                fingerMoved = false;
                fingerInContact = false;
            }
        }

        SwipeDelta = Vector2.zero;
        if (fingerInContact)
        {
            if (Input.GetMouseButton(0))
            {
                SwipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            if (Input.touches.Length > 0)
            {
                SwipeDelta = Input.touches[0].position - startTouch;
            }
        }

        if (SwipeDelta.sqrMagnitude > screenWidth)
        {
            // Direction check.
            float y = swipeDelta.y;
            if (y > 0 && !swipeUp)
            {
                fingerMoved = true;
                swipeUp = true;


                print("swipe up ");

                Reset();
            }
        }






        //if (Input.touches.Length > 0)
        //{
        //    if (Input.touches[0].phase == TouchPhase.Began)
        //    {
        //        fingerInContact = true;
        //        startTouch = Input.touches[0].position;
        //    }


        //    else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
        //    {
        //        fingerInContact = false;
        //        Reset();
        //    }
        //}

        //SwipeDelta = Vector2.zero;
        //if (fingerInContact)
        //{
        //    if (Input.touches.Length > 0)
        //    {
        //        SwipeDelta = Input.touches[0].position - startTouch;
        //    }

        //    if (Input.GetMouseButton(0))
        //    {
        //        SwipeDelta = (Vector2)Input.mousePosition - startTouch;
        //    }
        //}

        //if (SwipeDelta.sqrMagnitude > screenWidth)
        //{
        //    fingerStationary = false;

        //    // Direction check.
        //    float y = swipeDelta.y;
        //    if (y > 0)
        //    {
        //        swipeUp = true;
        //    }

        //    Reset();
        //}
    }

    public void Reset()
    {
        startTouch = SwipeDelta = Vector2.zero;
        fingerInContact = false;
        tap = false;
    }
}
