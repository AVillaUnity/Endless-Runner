using System.Collections;
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
    public Canvas InstructionCanvas;
    public Animator menuAnimator;

    private bool showedInstructions = false;

    private IEnumerator fadeOut;

    private void Start()
    {
        WorldCanvas.gameObject.SetActive(true);
        MainMenuCanvas.gameObject.SetActive(true);
        GameCanvas.gameObject.SetActive(true);
        LoseCanvas.gameObject.SetActive(true);
        PauseCanvas.gameObject.SetActive(true);
        InstructionCanvas.gameObject.SetActive(true);

        fadeOut = FadeOut();
    }

    public void ShowSplashScreen()
    {
        SplashScreen.gameObject.SetActive(true);
        SplashScreen.enabled = true;
        LoseCanvas.enabled = false;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = false;
        InstructionCanvas.enabled = false;
        MainMenuCanvas.enabled = true;

        Invoke("ShowMainMenu", SplashScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    public void ShowMainMenu()
    {
        StopAllCoroutines();

        SplashScreen.enabled = false;
        LoseCanvas.enabled = false;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = false;
        InstructionCanvas.enabled = false;
        MainMenuCanvas.enabled = true;
    }

    public void ShowGameCanvas()
    {
        StartCoroutine(fadeOut);

        SplashScreen.enabled = false;
        GameCanvas.enabled = true;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = true;

        if (!showedInstructions)
        {
            InstructionCanvas.enabled = true;
            InstructionCanvas.GetComponent<Animator>().SetTrigger("Fade");
            showedInstructions = true;
        }
    }

    public void ShowLoseCanvas()
    {
        StopAllCoroutines();

        SplashScreen.enabled = false;
        InstructionCanvas.enabled = false;
        LoseCanvas.enabled = true;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = false;
        WorldCanvas.enabled = false;
        MainMenuCanvas.enabled = false;
    }

    public void ShowPauseCanvas()
    {
        SplashScreen.enabled = false;
        InstructionCanvas.enabled = false;
        LoseCanvas.enabled = false;
        GameCanvas.enabled = false;
        PauseCanvas.enabled = true;
        WorldCanvas.enabled = true;
        MainMenuCanvas.enabled = false;
    }

    private IEnumerator FadeOut()
    {
        menuAnimator.SetTrigger("Fade Out");
        yield return new WaitForSeconds(1.0f);
        LoseCanvas.enabled = false;
        MainMenuCanvas.enabled = false;
        fadeOut = FadeOut();
    }
  
}
