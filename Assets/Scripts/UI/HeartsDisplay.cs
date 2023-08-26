using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsDisplay : MonoBehaviour
{
    public GameObject heartItem;

    private PlayerController _playerController;

    private List<GameObject> hearts;

    private RectTransform _rectTransform;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        if(_playerController == null) {
            Debug.LogError("No Player Controller found.");
            Destroy(gameObject);
        }
        hearts = new List<GameObject>();

        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        SpawnHeart(_playerController.GetHP());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
    }

    void UpdateHearts()
    {
        
        if(hearts.Count < _playerController.GetHP()){
            SpawnHeart(_playerController.GetHP() - hearts.Count);
        }else if(hearts.Count > _playerController.GetHP()){
            int diff = hearts.Count - _playerController.GetHP();
            for (int i = 0; i < diff; i++)
            {
                GameObject heart = hearts[i];
                hearts.RemoveAt(i);
                Destroy(heart);
            }
        }

        _rectTransform.sizeDelta = new Vector2(hearts.Count * 100, _rectTransform.sizeDelta.y);
    }


    void SpawnHeart(int count = 1){
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(heartItem, gameObject.transform);
            hearts.Add(obj);
        }
    }
}
