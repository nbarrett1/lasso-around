using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CowboyMovement cowboyMovement;

    [SerializeField]
    private GameObject SpherePoints;

    [SerializeField]
    private GameObject Horse;

    private GameObject spawnedHorse;
    private GameObject spawnedHazard;

    [SerializeField]
    private GameObject Hazard;

    [SerializeField]
    private List<Transform> PositionsOnSphere = new List<Transform>();

    [SerializeField]
    private List<Vector3> SphereRotations = new List<Vector3>();

    [SerializeField]
    private List<int> positions = new List<int>();

    [SerializeField]
    private int horseIsAtPosition = 0;

    [SerializeField]
    private int hazardIsAtPosition;

    // Sounds

    //Audio
    protected AudioSource audioPlayer;

    public AudioClip soundclip;





    // Game logic

    private int secondsLeft = 45;

    [SerializeField]
    private Text timeText;

    private int score = 0;

    [SerializeField]
    private Text scoreText;

    private int numberOfObjects = 5;
    private float radius = 3.8f;

    private void Awake()
    {
#if PLATFORM_IOS
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
#endif

        StartCoroutine(TickTock());
        StartCoroutine(spawningHazard());

        int rotation = -90;

        for (int i = 0; i < 10; i++)
        {
            float angle = i * Mathf.PI * 0.2f;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius * 4;
            GameObject spawnLocations = Instantiate(SpherePoints, pos, Quaternion.identity);

            PositionsOnSphere.Add(spawnLocations.transform);
            positions.Add(i + 1);

            spawnLocations.transform.parent = gameObject.transform;

            spawnLocations.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

            SphereRotations.Add(new Vector3(0, 0, rotation));

            rotation += 36;
        }

        scoreText.text = score.ToString();

        SpawnHorse();
    }

    public List<int> possiblePositions = new List<int>();

    public void SpawnHorse(bool forcedPosition = false)
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
            possiblePositions.RemoveAt(horseIsAtPosition);
            if (spawnedHazard != null)
            {
                possiblePositions.RemoveAt(hazardIsAtPosition);
            }

            // get random position but a valid one

            int randomPosition = possiblePositions[Random.Range(0, possiblePositions.Count)];

            horseIsAtPosition = randomPosition;

            spawnedHorse = Instantiate(Horse, PositionsOnSphere[randomPosition].localPosition, Quaternion.Euler(SphereRotations[randomPosition]));
            spawnedHorse.transform.parent = gameObject.transform;
        }
    }

    private void SpawnHazard()
    {
        possiblePositions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // remove existing position(s) from potentials
        possiblePositions.RemoveAt(horseIsAtPosition);

        if (hazardIsAtPosition != 10)
        {
            possiblePositions.RemoveAt(hazardIsAtPosition);
        }

        int randomPosition = possiblePositions[Random.Range(0, possiblePositions.Count)];

        hazardIsAtPosition = randomPosition;

        spawnedHazard = Instantiate(Hazard, PositionsOnSphere[randomPosition].localPosition, Quaternion.Euler(SphereRotations[randomPosition]));
        spawnedHazard.transform.parent = gameObject.transform;
    }

    public void DestroyHazard()
    {
        if (spawnedHazard != null)
        {
            Destroy(spawnedHazard, 0);

            hazardIsAtPosition = 10;
        }
    }

    public void AdjustScore(bool hasScored, int i)
    {
        if (hasScored)
        {
            score += i;
        }
        else score -= i;

        scoreText.text = score.ToString();
    }

    private IEnumerator spawningHazard()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            SpawnHazard();
            yield return new WaitForSeconds(Random.Range(3, 8));
            DestroyHazard();
        }
    }

    private IEnumerator TickTock()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
            timeText.text = secondsLeft.ToString();

            cowboyMovement.AdjustSpeed();

            if (secondsLeft.Equals(0))
            {
                // game over

                Destroy(cowboyMovement.gameObject);
                Destroy(gameObject);


                yield break;
            }
        }
    }
}