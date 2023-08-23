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

    void Attack(InputAction.CallbackContext context)
    {
        if(!_playerController.CanMove()) return;
        _playerController.Attacked();
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(_attackPosition, radius, hittableLayer);
        foreach(Collider2D hitObject in hitObjects)
        {
            hitObject.gameObject.GetComponent<Hittable>().Hit(1);
        }
    }

    void OnDrawGizmos(){
        _attackPosition = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
        Gizmos.DrawWireSphere(_attackPosition, radius);
    }
}
