using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject controlsButton;
    public GameObject exitButton;

    public GameObject lvl1Button;
    public GameObject lvl2Button;
    public GameObject lvl3Button;
    public GameObject backButton;

    
    public GameObject credit1Button;
    public GameObject credit2Button;
    public GameObject credit3Button;
    public GameObject backButton2;
    
    public GameObject moveButton;
    public GameObject punchButton;
    public GameObject jumpButton;
    public GameObject backButton3;

    void Awake(){
        Time.timeScale = 1;
        lvl1Button.transform.LeanSetLocalPosX(1300);
        lvl2Button.transform.LeanSetLocalPosX(1300);
        lvl3Button.transform.LeanSetLocalPosX(1300);
        backButton.transform.LeanSetLocalPosX(1300);
    }

    public void OpenControls()
    {
        playButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo();
        creditsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.05f);
        controlsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.1f);
        exitButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.15f);

        moveButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.2f);
        punchButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.25f);
        jumpButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.3f);
        backButton3.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
    }

    public void CloseControls()
    {

        moveButton.transform.LeanMoveLocalX(1500, 1f).setEaseOutExpo().setDelay(0.05f);
        punchButton.transform.LeanMoveLocalX(1500, 1f).setEaseOutExpo().setDelay(0.1f);
        jumpButton.transform.LeanMoveLocalX(1500, 1f).setEaseOutExpo().setDelay(0.15f);
        backButton3.transform.LeanMoveLocalX(1500, 1f).setEaseOutExpo().setDelay(0.2f);
        
        playButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.25f);
        creditsButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.3f);
        controlsButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
        exitButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.4f);
    }

    public void OpenCredits()
    {
        playButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo();
        creditsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.05f);
        controlsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.1f);
        exitButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.15f);

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
        controlsButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
        exitButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.4f);
    }

    public void OpenLevelSelect()
    {
        playButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo();
        creditsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.05f);
        controlsButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.1f);
        exitButton.transform.LeanMoveLocalX(1300, 1f).setEaseOutExpo().setDelay(0.15f);

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
        controlsButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.35f);
        exitButton.transform.LeanMoveLocalX(500, 1f).setEaseOutExpo().setDelay(0.4f);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

}
