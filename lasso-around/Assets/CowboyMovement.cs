using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject sprite;

    [SerializeField]
    private bool touchingHorse;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private Animator jumpAnimator;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private ShakeScript shaker;

    //Audio
    public AudioSource audioPlayer;

    public AudioClip horse;


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, speed * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        speed *= -1;

        if (speed < 0)
        {
            sprite.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
        }
        else sprite.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        // check horse collision

        if (touchingHorse)
        {
            touchingHorse = false;
            gameController.AdjustScore(true, 100);
            gameController.SpawnHorse();

            audioPlayer.PlayOneShot(horse);
        }
    }

    public void AdjustSpeed()
    {
        if (speed > 0)
        {
            speed += 2;
        }
        else speed -= 2;
    }

    public void Jump()
    {
        jumpAnimator.SetTrigger("jump");
        touchingHorse = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard")
        {
            gameController.AdjustScore(false, 100);

            gameController.DestroyHazard();

            shaker.Shake();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        touchingHorse |= other.tag == "Horse";
    }

    private void OnTriggerExit(Collider other)
    {
        touchingHorse = false;
    }
}
