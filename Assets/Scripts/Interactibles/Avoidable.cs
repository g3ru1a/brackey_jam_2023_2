using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidable : MonoBehaviour
{
    public AudioClip collidedAudioClip;
    [Range(0,1)]
    public float audioClipVolume = 1f;

    private GameManager _gameManager;

    private Vector2 _collided = Vector2.zero;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            _collided = collider.gameObject.transform.position;
            _gameManager.GetAudioSource().PlayOneShot(collidedAudioClip, audioClipVolume);
            collider.gameObject.GetComponent<PlayerController>().DisableMovement();
        }
    }

    void OnDrawGizmos(){
        if(_collided != Vector2.zero){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_collided, 0.5f);
        }
    }

}
