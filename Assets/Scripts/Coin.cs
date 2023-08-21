using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 50;
    private PointTracker pointTracker;

    private void Start()
    {
        pointTracker = FindObjectOfType<PointTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pointTracker.AddPoints(coinValue);
            Destroy(this.gameObject);
        }
    }
}
