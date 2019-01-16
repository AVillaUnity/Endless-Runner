using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI loseScoreText;
    public TextMeshProUGUI menuHighscoreText;
    public TextMeshProUGUI loseMenuHighscoreText;
    public TextMeshPro worldHighscoreText;
    public GameObject highscoreDisplay;

    private float score = 0.0f;
    private float highScore;
    private bool displayPlaced = false;
    private int difficulty = 1;
    private int maxDifficulty = 10;
    private int nextLevelAt = 100;
    private PlayerMovement player;
    private GameManager gameManager;
    private float highscorePositionOffset;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onReset += ResetTimer;

        highScore = PlayerPrefsManager.GetHighscore();
        UdpateHighScoreText();
        scoreText.text = FormatFloat(score).ToString();
        loseScoreText.text = scoreText.text;

        player = GameObject.FindObjectOfType<PlayerMovement>();

        GetInitialOffset();
        //ResetHighScore();
        highscoreDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.PlayerMoving){ return; }

        score += player.DistanceTraveled;

        scoreText.text = FormatFloat(score).ToString();
        loseScoreText.text = scoreText.text;

        //if(score >= nextLevelAt && difficulty < maxDifficulty)
        //{
        //    AdjustDifficulty();
        //}

        if (!displayPlaced && highscorePositionOffset != 0)
        {
            PlaceDisplayObject();
        }


    }

    private void PlaceDisplayObject()
    {
        Vector3 newPosition = highscoreDisplay.transform.position;
        newPosition.z = player.transform.position.z + highscorePositionOffset;
        highscoreDisplay.transform.position = newPosition;
        highscoreDisplay.SetActive(true);
        displayPlaced = true;
        
    }

    float FormatFloat(float num)
    {
        return (float)System.Math.Round(num, 2);
    }

    void AdjustDifficulty()
    {
        difficulty++;
        nextLevelAt *= 2;
        player.IncrementSpeed();
    }

    void ResetTimer()
    {
        if (score > highScore)
        {
            UpdateHighscore();
            CalculateDisplayOffset();
        }

        score = 0.0f;
        difficulty = 1;
        nextLevelAt = 100;
        scoreText.text = FormatFloat(score).ToString();
        displayPlaced = false;
    }

    private void UpdateHighscore()
    {
        float previousHighscore = highScore;
        highScore = score;
        PlayerPrefsManager.SetHighscore(highScore);
        UdpateHighScoreText();
    }

    private void UdpateHighScoreText()
    {
        worldHighscoreText.text = FormatFloat(highScore).ToString();
        menuHighscoreText.text = worldHighscoreText.text;
        loseMenuHighscoreText.text = worldHighscoreText.text;
    }

    void CalculateDisplayOffset()
    {
        float currentZPosition = player.transform.position.z;
        highscorePositionOffset = currentZPosition - player.StartingPosition.z;

        PlayerPrefsManager.SetOffsetZ(highscorePositionOffset);
    }

    void GetInitialOffset()
    {
        highscorePositionOffset = PlayerPrefsManager.GetOffsetZ(); ;
    }

    void ResetHighScore()
    {
        PlayerPrefsManager.SetHighscore(0);
        PlayerPrefsManager.SetOffsetZ(0);
        highScore = 0.0f;
        UdpateHighScoreText();
        highscorePositionOffset = 0;
        highscoreDisplay.SetActive(false);
    }
}
