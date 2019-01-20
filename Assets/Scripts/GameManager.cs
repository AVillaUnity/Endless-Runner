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
    private AudioManager audioManager;
    private BuildingOffset spawnPointBuildings;
    private bool isPaused = false;
    private PlayerMovement player;
    private CameraMovement cameraMovement;

    public delegate void OnReset();
    public OnReset onReset;

    public bool GameStarted { get; private set; }


    private void Start()
    {
        audioManager = AudioManager.instance;
        uIManager = UIManager.instance;
        player = GameObject.FindObjectOfType<PlayerMovement>();
        spawnPointBuildings = GameObject.FindObjectOfType<BuildingOffset>();
        cameraMovement = Camera.main.gameObject.GetComponent<CameraMovement>();

        GameStarted = false;
        //uIManager.ShowMainMenu();
        uIManager.ShowSplashScreen();
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

        if (!audioManager.ThemeIsPlaying) { audioManager.Play("Theme"); }
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
        StartCoroutine(ShowLoseScreen());
    }

    public void BackToMenu(bool needToWait)
    {
        if (cameraMovement.Transitioning) { return; }

        //musicPlaying = false;
        //audioSource.Stop();
        uIManager.ShowMainMenu();
        Time.timeScale = 1.0f;
        GameStarted = false;
        StartCoroutine(ResetStage(needToWait));
    }

    IEnumerator ResetStage(bool needToWait)
    {
        if (needToWait)
        {
            yield return new WaitForSeconds(cameraMovement.transitionSpeed);
        }

        onReset.Invoke();
        player.ResetPlayer();
        spawnPointBuildings.ChangePosition(player.transform.position);
    }

    IEnumerator ShowLoseScreen()
    {
        yield return new WaitForSeconds(1.0f);

        uIManager.ShowLoseCanvas();
        GameStarted = false;
        StartCoroutine(ResetStage(true));
    }



}
