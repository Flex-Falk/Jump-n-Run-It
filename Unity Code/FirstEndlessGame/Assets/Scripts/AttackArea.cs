using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
            Destroy(collision.gameObject);
            Debug.Log("kiste");
    }
}
