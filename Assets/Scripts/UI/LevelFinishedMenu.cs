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
        levelFinishMenu.SetActive(false);
        logoImage.transform.localScale = Vector2.zero;
        background.alpha = 0;
        background.interactable = false;
        score.alpha = 0;

        nextLevelButton.transform.LeanSetPosX(400);
        restartButton.transform.LeanSetPosX(400);
        menuButton.transform.LeanSetPosX(400);
        exitButton.transform.LeanSetPosX(400); 
    }


    public void OpenLevelFinishedMenu()
    {
        background.interactable = true;
        levelFinishMenu.SetActive(true);
        background.LeanAlpha(1, 0.6f).setEaseOutExpo().setIgnoreTimeScale(true);
        score.LeanAlpha(1, 0.6f).setEaseOutExpo().setIgnoreTimeScale(true);
        logoImage.transform.LeanScale(Vector2.one * 3, 0.2f).setEaseInOutQuart().setDelay(0.5f).setIgnoreTimeScale(true);


        nextLevelButton.transform.LeanMoveX(-50, 0.2f).setEaseInOutQuart().setDelay(0.55f).setIgnoreTimeScale(true);
        restartButton.transform.LeanMoveX(-50, 0.2f).setEaseInOutQuart().setDelay(0.6f).setIgnoreTimeScale(true);
        menuButton.transform.LeanMoveX(-50, 0.2f).setEaseInOutQuart().setDelay(0.65f).setIgnoreTimeScale(true);
        exitButton.transform.LeanMoveX(-50, 0.2f).setEaseInOutQuart().setDelay(0.7f).setIgnoreTimeScale(true);
        
    }
}
