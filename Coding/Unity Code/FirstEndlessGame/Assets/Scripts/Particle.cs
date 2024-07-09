using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
