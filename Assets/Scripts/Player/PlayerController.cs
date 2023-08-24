using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip jumpAudioClip;
    [Range(0,1)]
    public float jumpClipVolume = 1f;
    [SerializeField, Range(0,1)]
    private float _jumpAudioDelay = 0.4f;
    public AudioClip attackAudioClip;
    [Range(0,1)]
    public float attackClipVolume = 1f;
    public AudioClip deadAudioClip;
    [Range(0,1)]
    public float deadClipVolume = 1f;

    [SerializeField] private bool _canMove;

    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    private GameManager _gameManager;
    private Animator _animator;

    private CheckPoint _lastCheckPoint = null;

    private bool _isJumping = false;

    void Awake()
    {
        _playerCombat = GetComponent<PlayerCombat>();
        _playerMovement = GetComponent<PlayerMovement>();
        _gameManager = FindObjectOfType<GameManager>();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Jumped()
    {
        _gameManager.GetSFXSource().PlayOneShot(jumpAudioClip, jumpClipVolume);
    }

    public void Attacked()
    {
        _gameManager.GetSFXSource().PlayOneShot(attackAudioClip, attackClipVolume);
        _animator.SetTrigger("Attack");
    }

    public void PlayerDead()
    {
        _gameManager.GetSFXSource().PlayOneShot(deadAudioClip, deadClipVolume);
        _gameManager.GetTrackSource().Stop();
    }

    public void SetJumping(bool value)
    {
        _isJumping = value;
        _animator.SetBool("isJumping", value);
    }

    public bool IsJumping(){ return _isJumping;}

    public void EnableMovement() 
    {
        _canMove = true;
        _animator.SetBool("canMove", true);
    }
    public void DisableMovement() 
    {
        _canMove = false;
        _animator.SetBool("canMove", false);    
    }
    public bool CanMove() { return _canMove;}

    public float GetJumpAudioDelay() {return _jumpAudioDelay;}

    public void SetCheckpoint(CheckPoint checkPoint){
        _lastCheckPoint = checkPoint;
    }
}
