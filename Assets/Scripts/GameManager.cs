using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip track;
    public AudioSource trackSource;
    public float trackBPM = 128;
    [Range(0, 1)]
    public float trackOffset = 0;

    public AudioSource sfxSource;
    
    public TMP_Text pointsOnUI;

    public int totalPoints = 0;

    public GameObject pointLoosePS;

    
    private PlayerInputActions _inputActions;
    private InputAction _startAction;
    private InputAction _menuAction;

    private PlayerController _playerController;

    private PauseMenu _pauseMenu;

    private LevelLoader _levelLoader;


    void Awake()
    {
        _inputActions = new PlayerInputActions();
        GameManager[] gms = FindObjectsOfType<GameManager>();
        if(gms.Length > 1) Debug.LogError("Too Many Game Managers!");

        _playerController = FindObjectOfType<PlayerController>();
        _levelLoader = FindObjectOfType<LevelLoader>();
        _pauseMenu = GetComponent<PauseMenu>();
    }

    void OnEnable()
    {
        _startAction = _inputActions.Player.Start;
        _startAction.performed += StartGame;
        _startAction.Enable();
        _menuAction = _inputActions.Player.Menu;
        _menuAction.performed += MenuAction;
        _menuAction.Enable();
    }

    void OnDisable()
    {
        _startAction.Disable();
        _menuAction.Disable();
    }

    void StartGame(InputAction.CallbackContext context)
    {
        _playerController.EnableMovement();
        _startAction.Disable();
    }

    void MenuAction(InputAction.CallbackContext context)
    {
        _pauseMenu.TogglePauseMenu(this, _playerController);
    }

    public void RestartGame()
    {
        _levelLoader.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        _levelLoader.LoadLevel("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerController.DisableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            trackSource.clip = track;
            trackSource.Play();
        }
    }

    public void AddPoints(int points) { totalPoints += points; UpdatePointsUI(); }
    public void LoosePoints(bool allPoints = false) { 
        if(allPoints){
            if(totalPoints > 0 ){
                GameObject coinPS = Instantiate(pointLoosePS, _playerController.gameObject.transform.position, Quaternion.identity);
                ParticleSystem ps = coinPS.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.maxParticles = totalPoints;
                ps.Play();
            }
            totalPoints = 0;
        }else if (totalPoints > 0){
            int pointLoss = UnityEngine.Random.Range(1, totalPoints / 2);
            if(totalPoints > 0){
                GameObject coinPS = Instantiate(pointLoosePS, _playerController.gameObject.transform.position, Quaternion.identity);
                ParticleSystem ps = coinPS.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.maxParticles = pointLoss;
                ps.Play();
            }
            totalPoints -= pointLoss;
        }
        UpdatePointsUI(); 
    }

    public void UpdatePointsUI() { pointsOnUI.text = totalPoints.ToString(); }

    public int GetTotalPoints() { return totalPoints; }

    public void ResetPoints() { totalPoints = 0; UpdatePointsUI(); }

    public AudioSource GetSFXSource() { return sfxSource; }
    public AudioSource GetTrackSource() { return trackSource; }
}
