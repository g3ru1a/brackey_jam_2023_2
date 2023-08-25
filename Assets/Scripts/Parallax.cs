using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    public GameObject camera;

    public float parallaxEffect;

    private float _length, _startPos;

    

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr == null){
            TilemapRenderer tr = GetComponent<TilemapRenderer>();
            _length = tr.bounds.size.x;
        }else _length = sr.bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float dist = (camera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);
        if(temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos - _length) _startPos -= _length;
    }
}
