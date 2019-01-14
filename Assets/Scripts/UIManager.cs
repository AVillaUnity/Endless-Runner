using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Canvas SplashScreen;
    public Canvas WorldCanvas;
    public Canvas MainMenuCanvas;
    public Canvas GameCanvas;
    public Canvas LoseCanvas;
    public Canvas PauseCanvas;

    private void Start()
    {
        WorldCanvas.gameObject.SetActive(true);
        MainMenuCanvas.gameObject.SetActive(true);
        GameCanvas.gameObject.SetActive(true);
        LoseCanvas.gameObject.SetActive(true);
        PauseCanvas.gameObject.SetActive(true);
        //SplashScreen.gameObject.SetActive(true);
    }

    public void ShowSplashScreen()
    {
        SplashScreen.gameObject.SetActive(true);
        SplashScreen.enabled = true;
        LoseCanvas.enabled = false;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = false;
        MainMenuCanvas.enabled = true;

        Invoke("ShowMainMenu", SplashScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    public void ShowMainMenu()
    {
        SplashScreen.enabled = false;
        LoseCanvas.enabled = false;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = false;
        MainMenuCanvas.enabled = true;
    }

    public void ShowGameCanvas()
    {
        SplashScreen.enabled = false;
        LoseCanvas.enabled = false;
        GameCanvas.enabled = true;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = true;
        MainMenuCanvas.enabled = false;
    }

    public void ShowLoseCanvas()
    {
        SplashScreen.enabled = false;
        LoseCanvas.enabled = true;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = false;
        MainMenuCanvas.enabled = false;
    }

    public void ShowPauseCanvas()
    {
        SplashScreen.enabled = false;
        LoseCanvas.enabled = false;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = true;
        WorldCanvas.enabled = true;
        MainMenuCanvas.enabled = false;
    }
}
