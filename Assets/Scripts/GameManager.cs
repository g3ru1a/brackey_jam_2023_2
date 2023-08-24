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

    
    private PlayerInputActions _inputActions;
    private InputAction _startAction;
    private InputAction _restartAction;
    private InputAction _quitAction;

    private PlayerController _playerController;


    void Awake()
    {
        _inputActions = new PlayerInputActions();
        GameManager[] gms = FindObjectsOfType<GameManager>();
        if(gms.Length > 1) Debug.LogError("Too Many Game Managers!");

        _playerController = FindObjectOfType<PlayerController>();
    }

    void OnEnable()
    {
        _startAction = _inputActions.Player.Start;
        _startAction.performed += StartGame;
        _startAction.Enable();
        _restartAction = _inputActions.Player.Restart;
        _restartAction.performed += RestartGame;
        _restartAction.Enable();
        _quitAction = _inputActions.Player.Quit;
        _quitAction.performed += QuitGame;
        _quitAction.Enable();
    }

    void OnDisable()
    {
        _startAction.Disable();
        _restartAction.Disable();
    }

    void StartGame(InputAction.CallbackContext context)
    {
        _playerController.EnableMovement();
        _startAction.Disable();
    }

    void RestartGame(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitGame(InputAction.CallbackContext context)
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

    public void UpdatePointsUI() { pointsOnUI.text = totalPoints.ToString(); }

    public int GetTotalPoints() { return totalPoints; }

    public void ResetPoints() { totalPoints = 0; UpdatePointsUI(); }

    public AudioSource GetSFXSource() { return sfxSource; }
    public AudioSource GetTrackSource() { return trackSource; }
}
