using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.Instance.shieldPowerUp = true;
            PlayerManager.Instance.Shield();
            Destroy(gameObject);
        }
    }
}
