using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameObject : MonoBehaviour
{
    [Range(1, 20)]
    public float scrollSpeed = 5f;
    public Rigidbody2D _rb;

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector2.left * scrollSpeed;
    }
}
