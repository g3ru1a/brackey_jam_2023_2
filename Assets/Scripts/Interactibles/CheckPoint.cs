using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public AudioClip passedCheckPointClip;
    [Range(0,1)]
    public float volume = 1f;
    [Range(0,5)]
    public int HPBuff = 1;

    private float _clipTimeStamp;

    private GameManager _gameManager;
    private BoxCollider2D _boxCollider;

    private Vector2 _position;

    void Awake(){
        _gameManager = FindObjectOfType<GameManager>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _position = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Player") return;
        _gameManager.GetSFXSource().PlayOneShot(passedCheckPointClip, volume);
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        player.ReachedCheckpoint(this);
        _clipTimeStamp = _gameManager.trackSource.time;
        _boxCollider.enabled = false;
    }

    public Vector2 GetPosition() { return _position; }

    public float GetAudioTimeStamp() { return _clipTimeStamp; }

    void OnDrawGizmos(){
        // Gizmos.DrawWireSphere(gameObject.transform.position, .5f);
    }
}
