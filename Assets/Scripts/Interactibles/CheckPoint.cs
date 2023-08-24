using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public AudioClip passedCheckPointClip;
    [Range(0,1)]
    public float volume = 1f;

    private float _clipTimeStamp;

    private GameManager _gameManager;
    private BoxCollider2D _boxCollider;

    void Awake(){
        _gameManager = FindObjectOfType<GameManager>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Player") return;
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();

        _clipTimeStamp = _gameManager.trackSource.time;
        player.SetCheckpoint(this);
        _boxCollider.enabled = false;

    }
}
