using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private GameObject Horse;

    [SerializeField]
    private GameObject Bull;

    [SerializeField]
    private List<Transform> PositionsOnSphere = new List<Transform>();

    // Game logic

    private int secondsLeft = 45;


    private void Start()
    {
        GameObject spawnedHorse = Instantiate(Horse, PositionsOnSphere[0].localPosition, Quaternion.identity);
        spawnedHorse.transform.parent = gameObject.transform;

        //StartCoroutine(TickTock());
    }



    private void OMouseDown()
    {

    }







    private IEnumerator TickTock()
    {
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;

        if (secondsLeft.Equals(0))
        {
            // game over




            yield break;
        }
    }

}
