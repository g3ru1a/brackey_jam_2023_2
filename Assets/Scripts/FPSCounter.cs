using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{

    public static FPSCounter Instance;
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    public int avgFramerate = 0;
    public float m_refreshTime = 0.5f;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            avgFramerate = Mathf.FloorToInt(m_frameCounter / m_timeCounter);
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
    }
}
