using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJumpZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().SetCanJump(false);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().SetCanJump(true);
        }
    }
}
