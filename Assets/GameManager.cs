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

    public bool GameStarted { get; private set; }

    private UIManager uIManager;
    //private AudioSource audioSource;
    private bool isPaused = false;
    //private bool musicPlaying = false;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        uIManager = UIManager.instance;
        uIManager.ShowSplashScreen();
        //StartGame();
    }

    private void Update()
    {
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
        //ClearStage();

        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0;
    }

    public void BackToMenu()
    {
        //musicPlaying = false;
        //audioSource.Stop();
        uIManager.ShowMainMenu();
        ClearStage();
    }

    void ClearStage()
    {
        // To be implemented
    }

    void ResetPlayer()
    {
        PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();
    }

}
