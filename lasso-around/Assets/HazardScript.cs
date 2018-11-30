using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    [SerializeField]
    private SphereCollider collider;

    public AudioSource audio;

    public AudioClip spawn;

    private void Awake()
    {
        collider.enabled = false;
        Invoke("ActivateCollider", 1);
        audio.PlayOneShot(spawn);
    }

    private void ActivateCollider()
    {
        collider.enabled = true;
    }
}
