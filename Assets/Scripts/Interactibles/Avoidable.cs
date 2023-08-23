using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidable : MonoBehaviour
{
    public AudioClip collidedAudioClip;
    [Range(0,1)]
    public float audioClipVolume = 1f;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            _gameManager.GetAudioSource().PlayOneShot(collidedAudioClip, audioClipVolume);
            collision.gameObject.GetComponent<PlayerController>().DisableMovement();
        }
    }
}
