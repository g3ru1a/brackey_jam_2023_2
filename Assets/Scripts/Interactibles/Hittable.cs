using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public AudioClip hitAudioClip;
    [Range(0,1)]
    public float hitClipVolume = 1f;
    public AudioClip collidedAudioClip;
    [Range(0,1)]
    public float collidedClipVolume = 1f;

    [Range(1,5)]
    public int hitsToDestroy = 1;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Hit(int damage)
    {
        _gameManager.GetAudioSource().PlayOneShot(hitAudioClip,hitClipVolume);
        hitsToDestroy -= damage;
        if(hitsToDestroy <= 0){
            ObjectDestroyed();
        }
    }

    void ObjectDestroyed()
    {
        Debug.Log("Destroyed");
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            _gameManager.GetAudioSource().PlayOneShot(collidedAudioClip, collidedClipVolume);
            collision.gameObject.GetComponent<PlayerController>().DisableMovement();
        }
    }
}
