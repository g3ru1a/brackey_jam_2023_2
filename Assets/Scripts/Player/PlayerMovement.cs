using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    private Rigidbody2D _rigidBody;
    private CapsuleCollider2D _capsuleCollider;
    private PlayerController _playerController;

    private PlayerInputActions _playerControls;
    private InputAction _jumpInputAction;
    private GameManager _gameManager;

    private float _movementSpeed;

    private float _jumpHeight = 2f;
    private float _jumpDuration;
    private float _fallMultiplier = 12f;
    private Vector2 _fallForce;

    private float _jumpBuffer = 0.2f;
    private float _jumpBufferCounter = 0;
    private bool _isJumping = false;

    Vector2 startingPos = Vector2.zero;
    Vector2 endingPos = Vector2.zero;

    void Awake()
    {
        _playerControls = new PlayerInputActions();

        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
        _gameManager = FindObjectOfType<GameManager>();
        _playerController = gameObject.GetComponent<PlayerController>();

        float BPS = _gameManager.trackBPM / 60f;
        _movementSpeed = (BPS * 4) -  _gameManager.trackOffset;
        _jumpDuration = 60f /  _gameManager.trackBPM;
    }

    void OnEnable(){
        _jumpInputAction = _playerControls.Player.Jump;
        _jumpInputAction.performed += Jump;
        _jumpInputAction.Enable();
    }

    void Start()
    {
        MoveOnGround(true);
        _fallForce = Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1));
    }

    void Update()
    {
        if (_jumpBufferCounter > 0f)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (IsOnGround() && _jumpBufferCounter > 0f && !_isJumping && _playerController.CanMove())
        {
            StartCoroutine(JumpCoroutine());
        }

        if(_playerController.CanMove()){
            _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }else{
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void FixedUpdate()
    {
        if (_rigidBody.velocity.y < 0)
        {
            _rigidBody.AddForce(_fallForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        if (!_isJumping)
        {
            MoveTo(gameObject.transform.position + _movementSpeed * Vector3.right * Time.fixedDeltaTime);
        }
    }

    void OnDisable()
    {
        _jumpInputAction.Disable();
    }

    bool IsOnGround()
    {
        Vector2 groundCheckPosition = new Vector2(gameObject.transform.position.x, _capsuleCollider.bounds.min.y);
        return Physics2D.OverlapCircle(groundCheckPosition, 0.1f, _groundLayer);
    }

    void Jump(InputAction.CallbackContext context){
        _jumpBufferCounter = _jumpBuffer;
    }

    IEnumerator JumpCoroutine()
    {
        _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        _isJumping = true;
        bool playedSound = false;
        startingPos = new Vector2(gameObject.transform.position.x, _capsuleCollider.bounds.min.y);
        // endingPos = startingPos + (Vector2.right * (_movementSpeed * _jumpDuration)) - new Vector2(0.2f, 0);
        endingPos = startingPos + (Vector2.right * (_movementSpeed * _jumpDuration));

        Vector2 raycastOrigin = endingPos + Vector2.up * _jumpHeight;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, Mathf.Infinity, _groundLayer);

        float minYLand = (startingPos.y + _jumpHeight) - hit.distance;
        Vector2 adjustedEndingPos = new Vector2(endingPos.x, minYLand);

        float timeLeft = _jumpDuration;
        while (timeLeft >= 0f)
        {
            float normal = 1 - Mathf.InverseLerp(0, _jumpDuration, timeLeft);

            if(normal > _playerController.GetJumpAudioDelay() && !playedSound){
                _playerController.Jumped();
                playedSound = true;
            }

            float x = Mathf.Lerp(startingPos.x, endingPos.x, normal);
            float y = (Mathf.Sin(Mathf.Lerp(0, Mathf.PI, normal)) * _jumpHeight)
                + Mathf.Lerp(startingPos.y, adjustedEndingPos.y, normal);

            y += (_capsuleCollider.bounds.size.y / 2);
            // Debug.Log("Move To Y" + y + " | Current Y" + gameObject.transform.position.y);
            Vector3 newPosition = new Vector3(x, y, gameObject.transform.position.z);
            // MoveTo(newPosition);
            transform.position = newPosition;
            // Debug.Break();
            yield return new WaitForFixedUpdate();
            // timeLeft -= Time.fixedDeltaTime;
            timeLeft -= 0.01f;
        }
        _isJumping = false;
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        //Move to ground and take a step
        MoveOnGround();
        MoveTo(gameObject.transform.position + _movementSpeed * Vector3.right * Time.fixedDeltaTime);
    }

    void MoveOnGround(bool force = false)
    {
        Debug.Log("Move to ground");
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, _capsuleCollider.bounds.min.y),
            Vector2.down, Mathf.Infinity, _groundLayer);
        float groundY = _capsuleCollider.bounds.min.y - hit.distance + (_capsuleCollider.bounds.size.y / 2);
        MoveTo(new Vector3(transform.position.x, groundY, transform.position.z), force);
    }

    void MoveTo(Vector2 position, bool force = false)
    {
        if (_playerController.CanMove() || force)
        {
            _rigidBody.position = position;
        }
    }

    void OnDrawGizmos()
    {
        _capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
        _gameManager = FindObjectOfType<GameManager>();

        Vector2 groundCheckPosition = new Vector2(gameObject.transform.position.x, _capsuleCollider.bounds.min.y);
        Gizmos.DrawWireSphere(groundCheckPosition, 0.1f);

        float botY = _capsuleCollider.bounds.min.y;
        Vector2 pos1 = new Vector2(gameObject.transform.position.x - _capsuleCollider.bounds.size.x / 2, botY);
        Vector2 pos2 = new Vector2(gameObject.transform.position.x + _capsuleCollider.bounds.size.x / 2, botY);
        Gizmos.DrawLine(pos1, pos2);

        float BPS =  _gameManager.trackBPM / 60f;
        _movementSpeed = (BPS * 4) -  _gameManager.trackOffset;
        _jumpDuration = 60f /  _gameManager.trackBPM;


        // startingPos = new Vector2(gameObject.transform.position.x,
        //     gameObject.transform.position.y - _capsuleCollider.bounds.size.y / 2);
        // endingPos = startingPos +(Vector2.right * (_movementSpeed * _jumpDuration));

        //Flat sin arch
        Gizmos.color = Color.red;
        PlotSinCurveGizmo(startingPos, endingPos, _jumpDuration, Time.fixedDeltaTime);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startingPos, endingPos);

        //Adjusted for ground collision prediction
        Vector2 raycastOrigin = endingPos + Vector2.up * _jumpHeight;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, Mathf.Infinity, _groundLayer);
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + Vector2.down * hit.distance);

        float minYLand = (startingPos.y + _jumpHeight) - hit.distance;
        Vector2 adjustedEndingPos = new Vector2(endingPos.x, minYLand);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(startingPos, new Vector2(endingPos.x, minYLand));

        Gizmos.color = Color.green;
        PlotSinCurveGizmo(startingPos, adjustedEndingPos, _jumpDuration,
            Time.fixedDeltaTime, 0, _capsuleCollider.bounds.size.y / 2);

        Gizmos.DrawWireSphere(_capsuleCollider.transform.position,.5f);
    }

    void PlotSinCurveGizmo(Vector2 startingPos, Vector2 endingPos, float curveLength, float step, float xOffset = 0, float yOffset = 0)
    {
        Vector2 prev_pos = Vector2.zero;
        float remaining = _jumpDuration;
        while (remaining >= 0f)
        {
            float normal = 1 - Mathf.InverseLerp(0, curveLength, remaining);

            if (prev_pos == Vector2.zero)
            {
                prev_pos = GetNewPos(startingPos, endingPos, normal, xOffset, yOffset);
            }
            else
            {
                Vector2 new_pos = GetNewPos(startingPos, endingPos, normal, xOffset, yOffset);
                Gizmos.DrawLine(prev_pos, new_pos);
                prev_pos = new_pos;
            }
            remaining -= step;
        }
    }

    Vector2 GetNewPos(Vector2 startingPos, Vector2 endingPos, float normal, float xOffset = 0, float yOffset = 0)
    {
        float x = Mathf.Lerp(startingPos.x, endingPos.x, normal);
        float y = (Mathf.Sin(Mathf.Lerp(0, Mathf.PI, normal)) * _jumpHeight) + Mathf.Lerp(startingPos.y, endingPos.y, normal);
        return new Vector2(x + xOffset, y + yOffset);
    }
}