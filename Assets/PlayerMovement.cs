using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{

    public int trackBPM = 100;
    private float _movementSpeed;

    public InputAction inputAction;

    private float _jumpPower = 3.7f;
    private float _fallMultiplier = 8f;
    private Vector2 _fallForce;

    private Rigidbody2D _rigidBody;
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayer;

    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter = 0;
    private float _jumpBuffer = 0.2f;
    private float _jumpBufferCounter = 0;

    private bool _gameStarted = false;
    private bool _isJumping = false;

    void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _groundCheck = gameObject.transform.Find("GroundCheck").gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameStarted = true;
        inputAction.Enable();
        // Calculate Jump power and movement based on bpm
        _movementSpeed = Mathf.Sqrt(trackBPM)/2;
        // _jumpPower = _movementSpeed;

        _fallForce = Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1));
    }

    // Update is called once per frame
    void Update()
    {
        _fallForce = Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1));

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
    }

    void FixedUpdate(){
        // Multiply Fall Force
        if(_rigidBody.velocity.y < 0){
            _rigidBody.AddForce(_fallForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        _rigidBody.velocity = new Vector2(_movementSpeed, _rigidBody.velocity.y);
    }

    bool IsOnGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayer);;
    }

    void Jump(){
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpPower);
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
    }

    void OnDisable(){
        inputAction.Disable();
    }
}