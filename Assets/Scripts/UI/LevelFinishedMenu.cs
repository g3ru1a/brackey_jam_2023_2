using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishedMenu : MonoBehaviour
{
    public GameObject levelFinishMenu;
    public CanvasGroup background;
    public CanvasGroup score;
    public GameObject restartButton;
    public GameObject nextLevelButton;
    public GameObject menuButton;
    public GameObject exitButton;
    public GameObject logoImage;


    void Start()
    {
        
    }


    public void OpenPauseMenu()
    {
        background.interactable = true;
        Time.timeScale = 0;
        levelFinishMenu.SetActive(true);
        background.LeanAlpha(1, 0.6f).setEaseOutExpo().setIgnoreTimeScale(true);
        score.LeanAlpha(1, 0.6f).setEaseOutExpo().setIgnoreTimeScale(true);
        logoImage.transform.LeanScale(Vector2.one * 3, 0.2f).setEaseInOutQuart().setDelay(0.5f).setIgnoreTimeScale(true);


        nextLevelButton.transform.LeanMoveLocalX(-50, 0.2f).setEaseInOutQuart().setDelay(0.55f).setIgnoreTimeScale(true);
        restartButton.transform.LeanMoveLocalX(-50, 0.2f).setEaseInOutQuart().setDelay(0.6f).setIgnoreTimeScale(true);
        menuButton.transform.LeanMoveLocalX(-50, 0.2f).setEaseInOutQuart().setDelay(0.65f).setIgnoreTimeScale(true);
        exitButton.transform.LeanMoveLocalX(-50, 0.2f).setEaseInOutQuart().setDelay(0.7f).setIgnoreTimeScale(true);
        
    }
}
