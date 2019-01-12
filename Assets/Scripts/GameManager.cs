using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private UIManager uIManager;
    private BuildingOffset spawnPointBuildings;
    private bool isPaused = false;
    private PlayerMovement player;
    private CameraMovement cameraMovement;

    //private AudioSource audioSource;
    //private bool musicPlaying = false;

    public bool GameStarted { get; private set; }


    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        uIManager = UIManager.instance;
        player = GameObject.FindObjectOfType<PlayerMovement>();
        spawnPointBuildings = GameObject.FindObjectOfType<BuildingOffset>();
        cameraMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        player.onDeath += LoseGame;

        GameStarted = false;
        uIManager.ShowMainMenu();
        //uIManager.ShowSplashScreen();
        //StartGame();
    }

    private void Update()
    {
        if (!GameStarted) { return; }
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        if (cameraMovement.Transitioning) { return; }

        //if (!musicPlaying)
        //{
        //    audioSource.Play();
        //    musicPlaying = true;
        //}
        uIManager.ShowGameCanvas();
        GameStarted = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        isPaused = true;
        uIManager.ShowPauseCanvas();
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        uIManager.ShowGameCanvas();
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void LoseGame()
    {
        uIManager.ShowLoseCanvas();
        GameStarted = false;
        StartCoroutine(ResetStage());
    }

    public void BackToMenu()
    {
        if (cameraMovement.Transitioning) { return; }

        //musicPlaying = false;
        //audioSource.Stop();
        uIManager.ShowMainMenu();
        Time.timeScale = 1.0f;
        GameStarted = false;
        StartCoroutine(ResetStage());
    }

    IEnumerator ResetStage()
    {
        yield return new WaitForSeconds(cameraMovement.transitionSpeed);
        player.ResetPlayer();
        spawnPointBuildings.ChangePosition(player.transform.position);
    }



}
