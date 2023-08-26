using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject exitButton;

    public GameObject lvl1Button;
    public GameObject lvl2Button;
    public GameObject lvl3Button;
    public GameObject backButton;

    
    public GameObject credit1Button;
    public GameObject credit2Button;
    public GameObject credit3Button;
    public GameObject backButton2;

    void Awake(){
        Time.timeScale = 1;
        lvl1Button.transform.LeanSetLocalPosX(1300);
        lvl2Button.transform.LeanSetLocalPosX(1300);
        lvl3Button.transform.LeanSetLocalPosX(1300);
        backButton.transform.LeanSetLocalPosX(1300);
    }

    public void OpenCredits()
    {
        playButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo();
        creditsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.05f);
        exitButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.1f);

        credit1Button.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.2f);
        credit2Button.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.25f);
        credit3Button.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.3f);
        backButton2.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
    }

    public void CloseCredits()
    {

        credit1Button.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.05f);
        credit2Button.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.1f);
        credit3Button.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.15f);
        backButton2.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.2f);
        
        playButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.25f);
        creditsButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.3f);
        exitButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
    }

    public void OpenLevelSelect()
    {
        playButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo();
        creditsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.05f);
        exitButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.1f);

        lvl1Button.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.2f);
        lvl2Button.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.25f);
        lvl3Button.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.3f);
        backButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
    }

    public void CloseLevelSelect()
    {
        lvl1Button.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.05f);
        lvl2Button.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.1f);
        lvl3Button.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.15f);
        backButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.2f);
        
        playButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.25f);
        creditsButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.3f);
        exitButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

}
