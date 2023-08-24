using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Range(0,1)]
    public float offsetX = 0;
    [Range(0,1)]
    public float offsetY = 0;
    [Range(0,1)]
    public float radius = 1f;

    public LayerMask hittableLayer;

    private Vector2 _attackPosition;

    private PlayerInputActions _inputActions;
    private InputAction _attackAction;
    private PlayerController _playerController;
    
    void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        _inputActions = new PlayerInputActions();
        _attackPosition = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
    }

    void OnEnable()
    {
        _attackAction = _inputActions.Player.Attack;
        _attackAction.performed += Attack;
        _attackAction.Enable();
    }

    void OnDisable()
    {
        _attackAction.Disable();
    }

    IEnumerator AttackRoutine()
    {
        int attackFrames = 4;
        while(attackFrames > 0){

            Collider2D[] hitObjects = Physics2D.OverlapBoxAll(_attackPosition, Vector2.one*radius, 0, hittableLayer);
            Debug.Log(hitObjects.Length);
            if(hitObjects.Length > 0){
                foreach(Collider2D hitObject in hitObjects)
                {
                    hitObject.gameObject.GetComponent<Hittable>().Hit(1);
                }
            }
            attackFrames--;
            yield return new WaitForFixedUpdate();
        }
    }

    void Attack(InputAction.CallbackContext context)
    {
        if(!_playerController.CanMove()) return;
        _attackPosition = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
        _playerController.Attacked();
        StartCoroutine(AttackRoutine());
    }

    void OnDrawGizmos(){
        _attackPosition = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
        Gizmos.DrawWireCube(_attackPosition, Vector3.one * radius);
    }
}
