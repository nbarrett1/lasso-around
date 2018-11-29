using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeFunction : MonoBehaviour
{
    public swipeScript swipeControls;
    bool inContact;

    private void FixedUpdate()
    {
        if (swipeControls.Tap && !swipeControls.SwipeUp)
        {
            StartCoroutine(WaitForPotentialSwipe());
        }

        if (swipeControls.SwipeUp && inContact)
        {
            inContact = false;
            print("Swipe up");
        }
    }

    private IEnumerator WaitForPotentialSwipe()
    {
        inContact = true;

        yield return new WaitForSeconds(0.1f);

        if (!swipeControls.SwipeUp && inContact)
        {
            print("Tap");
        }

        inContact = false;
        yield break;
    }
}