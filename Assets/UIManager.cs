using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    //public GameObject SplashScreen;
    public GameObject WorldCanvas;
    public GameObject MainMenuCanvas;
    public GameObject GameCanvas;
    public GameObject LoseCanvas;
    public GameObject PauseCanvas;

    //public void ShowSplashScreen()
    //{
    //    SplashScreen.SetActive(true);
    //    LoseCanvas.SetActive(false);
    //    GameCanvas.SetActive(false);
    //    PauseCanvas.SetActive(false);
    //    MainMenuCanvas.SetActive(false);

    //    Invoke("ShowMainMenu", SplashScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    //}

    public void ShowMainMenu()
    {
        //SplashScreen.SetActive(false);
        LoseCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        WorldCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void ShowGameCanvas()
    {
        //SplashScreen.SetActive(false);
        LoseCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        WorldCanvas.SetActive(true);

        PauseCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);
    }

    public void ShowLoseCanvas()
    {
        //SplashScreen.SetActive(false);
        LoseCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        WorldCanvas.SetActive(false);

        PauseCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);
    }

    public void ShowPauseCanvas()
    {
        //SplashScreen.SetActive(false);
        LoseCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        WorldCanvas.SetActive(true);

        PauseCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }
}
