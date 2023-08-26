using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public CanvasGroup background;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject exitButton;
    public GameObject logoImage;
    
    public bool isMenuOpen = false;

    private GameManager _gm;
    private PlayerController _pc;
    private bool _wasMoving = false;
    private bool _wasPlaying = false;

    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        logoImage.transform.localScale = Vector2.zero;
        resumeButton.transform.localScale = Vector2.zero;
        restartButton.transform.localScale = Vector2.zero;
        menuButton.transform.localScale = Vector2.zero;
        exitButton.transform.localScale = Vector2.zero;
        background.alpha = 0;
        background.interactable = false;
    }

    public void TogglePauseMenu(GameManager gm, PlayerController pc)
    {
        
        if(isMenuOpen) ClosePauseMenu();
        else OpenPauseMenu(gm, pc);
    }
    public void OpenPauseMenu(GameManager gm, PlayerController pc)
    {
        _gm = gm;
        _pc = pc;
        _wasMoving = _pc.CanMove();
        _wasPlaying = gm.trackSource.isPlaying;
        isMenuOpen = true;
        background.interactable = true;
        _pc.DisableMovement();
        _gm.trackSource.Pause();
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        background.LeanAlpha(1, 0.6f).setEaseOutExpo().setIgnoreTimeScale(true);
        logoImage.transform.LeanScale(Vector2.one * 3, 0.2f).setEaseInOutQuart().setDelay(0.5f).setIgnoreTimeScale(true);
        resumeButton.transform.LeanScale(Vector2.one * 2.5f, 0.2f).setEaseInOutQuart().setDelay(0.55f).setIgnoreTimeScale(true);
        restartButton.transform.LeanScale(Vector2.one * 2.5f, 0.2f).setEaseInOutQuart().setDelay(0.6f).setIgnoreTimeScale(true);
        menuButton.transform.LeanScale(Vector2.one * 2.5f, 0.2f).setEaseInOutQuart().setDelay(0.65f).setIgnoreTimeScale(true);
        exitButton.transform.LeanScale(Vector2.one * 2.5f, 0.2f).setEaseInOutQuart().setDelay(0.7f).setIgnoreTimeScale(true);
        
    }

    IEnumerator ResumeGame(float delay){
        yield return new WaitForSecondsRealtime(delay);
        background.interactable = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        if(_wasMoving) _pc.EnableMovement();
        if(_wasPlaying) _gm.trackSource.UnPause();
    }

    public void ClosePauseMenu()
    {
        StartCoroutine(ResumeGame(0.7f));
        isMenuOpen = false;
        background.interactable = false;
        logoImage.transform.LeanScale(Vector2.zero, 0.2f).setDelay(0f).setIgnoreTimeScale(true);
        resumeButton.transform.LeanScale(Vector2.zero, 0.2f).setDelay(0.05f).setIgnoreTimeScale(true);
        restartButton.transform.LeanScale(Vector2.zero, 0.2f).setDelay(0.1f).setIgnoreTimeScale(true);
        menuButton.transform.LeanScale(Vector2.zero, 0.2f).setDelay(0.15f).setIgnoreTimeScale(true);
        exitButton.transform.LeanScale(Vector2.zero, 0.2f).setDelay(0.2f).setIgnoreTimeScale(true);
        background.LeanAlpha(0, 0.5f).setEaseOutExpo().setDelay(0.2f).setIgnoreTimeScale(true);
        
        // pauseMenu.SetActive(false);
    }
}
