using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterParticles : MonoBehaviour
{
    void OnParticleSystemStopped(){
        Destroy(gameObject);
    }
}
