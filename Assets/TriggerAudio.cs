using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{

    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.layer == 3){
            audioSource.Play();
        }
    }
}
