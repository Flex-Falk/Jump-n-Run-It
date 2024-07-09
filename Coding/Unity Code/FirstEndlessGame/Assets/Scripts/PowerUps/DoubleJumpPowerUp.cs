using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPowerUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.doubleJumpPowerUp = true;
            Destroy(gameObject);
        }
    }
}
