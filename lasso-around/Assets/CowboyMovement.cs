using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, speed * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        speed *= -1;
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y *-1F, transform.localScale.z);
    }
}
