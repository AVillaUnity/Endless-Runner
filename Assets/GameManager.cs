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

    //private AudioSource audioSource;
    //private bool musicPlaying = false;

    public bool GameStarted { get; private set; }


    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        uIManager = UIManager.instance;
        player = GameObject.FindObjectOfType<PlayerMovement>();
        spawnPointBuildings = GameObject.FindObjectOfType<BuildingOffset>();

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

    public IEnumerator LoseGame()
    {
        uIManager.ShowLoseCanvas();
        GameStarted = false;
        ResetStage();

        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0;
    }

    public void BackToMenu()
    {
        //musicPlaying = false;
        //audioSource.Stop();
        uIManager.ShowMainMenu();
        GameStarted = false;
        ResetStage();
    }

    void ResetStage()
    {
        player.ResetPlayer();
        spawnPointBuildings.ChangePosition(player.transform.position);

    }


}
