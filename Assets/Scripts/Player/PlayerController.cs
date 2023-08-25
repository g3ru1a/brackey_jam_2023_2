using System.Collections;
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
    public AudioClip failAudioClip;
    [Range(0,1)]
    public float failClipVolume = 1f;
    public AudioClip deadAudioClip;
    [Range(0,1)]
    public float deadClipVolume = 1f;

    [SerializeField] private bool _canMove;

    public float backToCheckpointTransitionDurationSeconds = 2f;

    private int _playerHP = 1;
    private int _maxPlayerHP = 1;

    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    private GameManager _gameManager;
    private Animator _animator;
    private CapsuleCollider2D _collider;
    private Rigidbody2D _rigidBody;

    private CheckPoint _lastCheckPoint = null;

    private bool _isJumping = false;

    void Awake()
    {
        _playerCombat = GetComponent<PlayerCombat>();
        _playerMovement = GetComponent<PlayerMovement>();
        _gameManager = FindObjectOfType<GameManager>();
        _animator = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<CapsuleCollider2D>();
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
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

    public void PlayerDeath(){
        _gameManager.GetSFXSource().PlayOneShot(deadAudioClip, deadClipVolume);
        _gameManager.GetTrackSource().Stop();
    }

    public void PlayerFailed()
    {
        _playerHP--;
        if(_playerHP <= 0){
            _gameManager.LoosePoints(true);
            PlayerDeath();
            return;
        }

        if(_lastCheckPoint != null)
        {
            _gameManager.LoosePoints();
            _gameManager.GetSFXSource().PlayOneShot(failAudioClip, failClipVolume);
            StartCoroutine(BackToCheckpoint(_lastCheckPoint));
        }
    }

    IEnumerator BackToCheckpoint(CheckPoint checkPoint)
    {
        float colliderHeight = _collider.bounds.size.y;
        _collider.enabled = false;
        DisableMovement();
        _gameManager.GetTrackSource().Pause();
        yield return new WaitForSeconds(2);
        Vector2 playerInitialPosition = gameObject.transform.position;
        Debug.Log(colliderHeight);
        Vector2 playerFinalPosition = _playerMovement.GetGroundAt(checkPoint.GetPosition() + Vector2.up * colliderHeight, colliderHeight/2);
        Debug.Log(playerFinalPosition);
        float timeLeft = backToCheckpointTransitionDurationSeconds;
        float x, y = 0;
        while(timeLeft >= 0)
        {
            float normal = 1 - Mathf.InverseLerp(0, backToCheckpointTransitionDurationSeconds, timeLeft);

            x = Mathf.Lerp(playerInitialPosition.x, playerFinalPosition.x, normal);
            y = Mathf.Lerp(playerInitialPosition.y, playerFinalPosition.y, normal);

            _rigidBody.position = new Vector2(x, y);

            yield return new WaitForFixedUpdate();
            timeLeft -= Time.fixedDeltaTime;
        }
        x = Mathf.Lerp(playerInitialPosition.x, playerFinalPosition.x, 1);
        y = Mathf.Lerp(playerInitialPosition.y, playerFinalPosition.y, 1);
        _rigidBody.position = new Vector2(x, y);

        GameObject[] hittables = GameObject.FindGameObjectsWithTag("Hittable");
        foreach(GameObject obj in hittables){
            obj.GetComponent<Hittable>().EnableObject();
        }
        yield return new WaitForSeconds(2);
        _collider.enabled = true;
        _gameManager.GetTrackSource().time = checkPoint.GetAudioTimeStamp();
        _gameManager.GetTrackSource().Play();
        EnableMovement();
    }

    public void ReachedCheckpoint(CheckPoint checkPoint){
        _playerHP += checkPoint.HPBuff;
        _maxPlayerHP += checkPoint.HPBuff;
        _lastCheckPoint = checkPoint;

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

    public int GetHP() { return _playerHP; }
    public int GetMaxHP() { return _maxPlayerHP; }

    void OnDrawGizmos(){
        if(_lastCheckPoint && _collider){
            // Debug.Log(_playerMovement.GetGroundAt(_lastCheckPoint.GetPosition() + Vector2.up * _collider.bounds.size.y));
            // Vector2 pos = _lastCheckPoint.GetPosition() + Vector2.up * _collider.bounds.size.y;
            // Debug.Log("Checkpoint Pos: " + _lastCheckPoint.GetPosition());
            // Debug.Log("Adjusted Pos: " + pos);
            // Debug.Log("Ground Detected Pos: " + _playerMovement.GetGroundAt(pos));

            // Gizmos.DrawLine(pos, _playerMovement.GetGroundAt(pos));
            // Gizmos.DrawWireSphere(_playerMovement.GetGroundAt(pos), 1f);
        }
    }

}
