using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIDisplayHearts : MonoBehaviour
{
    public GameObject heartObject;
    public Sprite emptyHeart;
    private GameObject[] _hearts;

    private PlayerController _playerController;

    void Start()
    {
        _hearts = new GameObject[50];
    }

    
    public void AddHearts(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject heart = Instantiate(heartObject, gameObject.transform);
            _hearts[_hearts.Length] = heart;
        }
    }

    public void SetHpStatus(int emptyHeartsCount){
        for(int i = 0; i < emptyHeartsCount; i++){
            _hearts[i].GetComponent<Image>().sprite = emptyHeart;
        }
    }
}
