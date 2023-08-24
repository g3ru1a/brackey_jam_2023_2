using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidable : MonoBehaviour
{
    private Vector2 _collided = Vector2.zero;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            _collided = collider.gameObject.transform.position;
            
            PlayerController pc = collider.gameObject.GetComponent<PlayerController>();
            pc.DisableMovement();
            pc.PlayerDead();
        }
    }

    void OnDrawGizmos(){
        if(_collided != Vector2.zero){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_collided, 0.5f);
        }
    }

}
