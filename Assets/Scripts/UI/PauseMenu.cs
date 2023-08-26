using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject background;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject exitButton;
    public GameObject logoImage;
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void OpenPauseMenu()
    {

        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {

        pauseMenu.SetActive(false);
    }
}
