using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip jumpAudioClip;
    [Range(0,1)]
    public float jumpClipVolume = 1f;

    [SerializeField] private bool _canMove;

    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    private GameManager _gameManager;

    void Awake()
    {
        _playerCombat = GetComponent<PlayerCombat>();
        _playerMovement = GetComponent<PlayerMovement>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Jumped(){
        _gameManager.GetAudioSource().PlayOneShot(jumpAudioClip, jumpClipVolume);
    }

    public void EnableMovement() {_canMove = true;}
    public void DisableMovement() {_canMove = false;}
    public bool CanMove() { return _canMove;}
}
