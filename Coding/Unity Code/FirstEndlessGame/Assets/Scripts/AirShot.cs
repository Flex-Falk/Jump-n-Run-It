using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShot : MonoBehaviour
{
    private Vector3 spawnPosition;
    private float maxDistance = 35f;
   
    private void Start() {
    }

    void Update()
    {
        /*float traveledDistance = Vector3.Distance(transform.position, spawnPosition);
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }*/
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Breakable")
        {
            Debug.Log("juhsdfjhufsdhju");
            PlayerManager.Instance.Crate(collision.gameObject.transform);
            Destroy(collision.gameObject);
            //Destroy(gameObject);
            //gameObject.SetActive(false);

        }

        if(collision.gameObject.tag == "Obstacle"){
            //Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Breakable")
        {
            PlayerManager.Instance.Crate(other.gameObject.transform);
            Destroy(other.gameObject);
            //Destroy(gameObject);
            //gameObject.SetActive(false);

        }

        if (other.gameObject.tag == "Obstacle")
        {
            //Destroy(gameObject);
        }
    }
} 