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

        nextLevelButton.transform.localScale = Vector2.zero; 
        restartButton.transform.localScale = Vector2.zero;
        menuButton.transform.localScale = Vector2.zero;
        exitButton.transform.localScale = Vector2.zero; 
    }


    public void OpenLevelFinishedMenu()
    {
        background.interactable = true;
        levelFinishMenu.SetActive(true);
        background.LeanAlpha(1, 0.6f).setEaseOutExpo().setIgnoreTimeScale(true);
        score.LeanAlpha(1, 0.6f).setEaseOutExpo().setIgnoreTimeScale(true);
        logoImage.transform.LeanScale(Vector2.one * 3, 0.2f).setEaseInOutQuart().setIgnoreTimeScale(true);


        restartButton.transform.LeanScale(Vector2.one * 2f, 0.2f).setEaseInOutQuart().setDelay(0.55f).setIgnoreTimeScale(true);
        nextLevelButton.transform.LeanScale(Vector2.one * 2f, 0.2f).setEaseInOutQuart().setDelay(0.6f).setIgnoreTimeScale(true);
        menuButton.transform.LeanScale(Vector2.one * 2f, 0.2f).setEaseInOutQuart().setDelay(0.65f).setIgnoreTimeScale(true);
        exitButton.transform.LeanScale(Vector2.one * 2f, 0.2f).setEaseInOutQuart().setDelay(0.7f).setIgnoreTimeScale(true);
        
    }
}
