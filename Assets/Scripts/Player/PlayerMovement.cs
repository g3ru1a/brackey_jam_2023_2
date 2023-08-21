using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public AudioSource audioSource;
    public Transform startingPoint;
    public Transform endingPoint;

    private float _movementSpeed;
    private float _audioSpeed;

    [SerializeField]
    private LayerMask _groundLayer;
    private Rigidbody2D _rigidBody;
    private Transform _groundCheck;
    
    private float jumpHeight = .7f;
    private float _jumpPower;
    private float _fallMultiplier = 12f;
    private Vector2 _fallForce;

    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter = 0;
    private float _jumpBuffer = 0.2f;
    private float _jumpBufferCounter = 0;
    private bool _isJumping = false;

    private bool _gameStarted = false;

    void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _groundCheck = gameObject.transform.Find("GroundCheck").gameObject.transform;
    }

    void Start()
    {
        _gameStarted = true;
        
        float distance = endingPoint.position.x - startingPoint.position.x; // distance in units
        float time = audioSource.clip.length; // time in seconds
        _audioSpeed = distance/time;

        _movementSpeed = 0f;
        // Debug.Log(_movementSpeed);
        _jumpPower = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * _rigidBody.gravityScale) * -2) * _rigidBody.mass;
        _fallForce = Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1));
    }

    void Update()
    {
        
        // Debug.Log(audioSource.time + " || " + audioSource.timeSamples);
        // To be removed once tweaking is complete
        _fallForce = Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1));
        // .\
        if(Keyboard.current.spaceKey.wasPressedThisFrame ||
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            _jumpBufferCounter = _jumpBuffer;
        }else if (_jumpBufferCounter > 0f){
            _jumpBufferCounter -= Time.deltaTime;
        }

        if(IsOnGround()){
            _coyoteTimeCounter = _coyoteTime;
        }else if (_coyoteTimeCounter > 0f){
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if(_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && !_isJumping){
            Jump();
            StartCoroutine(JumpCooldown());
        }

        if(Keyboard.current.rKey.wasPressedThisFrame){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate(){
        // Multiply Fall Force
        if(_rigidBody.velocity.y < 0){
            _rigidBody.AddForce(_fallForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        // _rigidBody.velocity = new Vector2(5, _rigidBody.velocity.y);
        if(audioSource.time != 0){
            gameObject.transform.position += _audioSpeed * Vector3.right * Time.deltaTime;
        }else{
            gameObject.transform.position += _movementSpeed * Vector3.right * Time.deltaTime;
        }
    }

    bool IsOnGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayer);;
    }

    void Jump(){
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        _rigidBody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    IEnumerator JumpCooldown()
    {
        _isJumping = true;
        yield return new WaitForSeconds(0.4f);
        _isJumping = false;
    }

    void OnDrawGizmos()
    {
        if(_gameStarted){
            Gizmos.DrawWireSphere(_groundCheck.position, 0.1f);
        }
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + Vector3.right);
    }
}