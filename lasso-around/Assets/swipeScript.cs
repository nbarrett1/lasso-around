using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeScript : MonoBehaviour
{
    protected bool tap, swipeUp;
    public bool Tap { get { return tap; } }
    public bool SwipeUp { get { return swipeUp; } }

    public bool fingerInContact;

    private Vector2 startTouch, SwipeDelta;
    public Vector2 swipeDelta { get { return SwipeDelta; } }

    public int screenWidth;

    private void Start()
    {
        screenWidth = Screen.height / 10;
    }

    private void Update()
    {
        swipeUp = tap = false;

        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
            fingerInContact = true;
            tap = true;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            fingerInContact = false;
            tap = false;
            Reset();
        }

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                fingerInContact = true;
                startTouch = Input.touches[0].position;
            }

            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                fingerInContact = false;
                Reset();
            }
        }

        SwipeDelta = Vector2.zero;
        if (fingerInContact)
        {
            if (Input.touches.Length > 0)
            {
                SwipeDelta = Input.touches[0].position - startTouch;
            }

            if (Input.GetMouseButton(0))
            {
                SwipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (SwipeDelta.sqrMagnitude > screenWidth)
        {
            // Direction check.
            float y = swipeDelta.y;
            if (y > 0)
            {
                swipeUp = true;
            }

            Reset();
        }
    }

    public void Reset()
    {
        startTouch = SwipeDelta = Vector2.zero;
        fingerInContact = false;
    }
}
