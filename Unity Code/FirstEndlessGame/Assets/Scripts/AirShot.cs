using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShot : MonoBehaviour
{
    private Vector3 spawnPosition;
    private float maxDistance = 35f;

    private void Start() {
        spawnPosition = transform.position;
    }

       void Update()
    {
        float traveledDistance = Vector3.Distance(transform.position, spawnPosition);
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Breakable")
        {
            PlayerManager.Instance.Crate(collision.gameObject.transform);
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }

        if(collision.gameObject.tag == "Obstacle"){
            Destroy(gameObject);
        }
    }
} 