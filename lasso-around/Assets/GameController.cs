using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CowboyMovement cowboyMovement;

    [SerializeField]
    private GameObject SpherePoints;

    [SerializeField]
    private GameObject Horse;

    private GameObject spawnedHorse;

    [SerializeField]
    private GameObject Bull;

    [SerializeField]
    private List<Transform> PositionsOnSphere = new List<Transform>();

    [SerializeField]
    private List<int> positions = new List<int>();

    [SerializeField]
    private int horseIsAtPosition = 0;

    [SerializeField]
    private int bullIsAtPosition;

    // Game logic

    private int secondsLeft = 45;

    private int numberOfObjects = 5;
    private float radius = 5;

    private void Awake()
    {
#if PLATFORM_IOS
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
#endif

        //GameObject spawnedHorse = Instantiate(Horse, PositionsOnSphere[0].localPosition, Quaternion.identity);
        //spawnedHorse.transform.parent = gameObject.transform;
        //StartCoroutine(TickTock());

        for (int i = 0; i < 10; i++)
        {
            float angle = i * Mathf.PI * 0.2f;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius * 4;
            GameObject spawnLocations = Instantiate(SpherePoints, pos, Quaternion.identity);

            PositionsOnSphere.Add(spawnLocations.transform);
            positions.Add(i + 1);

            spawnLocations.transform.parent = gameObject.transform;
        }

        SpawnHorse(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            //SpawnHorse();
            //cowboyMovement.ChangeDirection();
        }
    }

    private void OMouseDown()  // 
    {
        print("mouse down");

        // SpawnHorse();
    }

    public List<int> possiblePositions = new List<int>();

    private void SpawnHorse(bool forcedPosition = false)
    {
        if (spawnedHorse != null)
        {
            Destroy(spawnedHorse, 0);
        }

        if (forcedPosition == true)
        {
            horseIsAtPosition = 0;

            spawnedHorse = Instantiate(Horse, PositionsOnSphere[0].localPosition, Quaternion.identity);
        }
        else
        {
            possiblePositions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // remove existing position(s) from potentials
            // TODO: find new position with 4 either side


            possiblePositions.RemoveAt(horseIsAtPosition);

            // get random position but a valid one

            int randomPosition = possiblePositions[Random.Range(0, possiblePositions.Count)];

            horseIsAtPosition = randomPosition;

            spawnedHorse = Instantiate(Horse, PositionsOnSphere[randomPosition].localPosition, Quaternion.identity);

            //print(" horse position " + horseIsAtPosition);
        }
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
