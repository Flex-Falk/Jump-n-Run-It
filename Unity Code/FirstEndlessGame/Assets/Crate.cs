using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip breakClip;

    void Start(){

        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "AttackArea")
        {
            Destroy(gameObject);
            audioSource.PlayOneShot(breakClip);

        }
    }
}