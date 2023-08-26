using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHUDItem : MonoBehaviour
{
    
    public Sprite heartFull;
    public Sprite heartEmpty;

    private Image _image;

    private bool _isEmpty=false;

    void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = heartFull;
        _isEmpty=false;
    }

    public bool IsEmpty()
    {
        return _isEmpty;
    }

    public void SetEmpty()
    {
        _image.sprite = heartEmpty;
        _isEmpty=true;
    }

    public void SetFull()
    {
        _image.sprite = heartFull;
        _isEmpty=false;
    }
}
