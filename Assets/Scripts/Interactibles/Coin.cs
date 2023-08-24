using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 50;
    public AudioClip pickupAudioClip;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _gameManager.GetSFXSource().PlayOneShot(pickupAudioClip);
            _gameManager.AddPoints(coinValue);
            Destroy(this.gameObject);
        }
    }
}
