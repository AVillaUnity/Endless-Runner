using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshPro highScoreText;
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

        highScore = PlayerPrefsManager.GetHighscore();
        highScoreText.text = FormatFloat(highScore).ToString();
        scoreText.text = FormatFloat(score).ToString();

        player = GameObject.FindObjectOfType<PlayerMovement>();
        player.onDeath += ResetTimer;

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

        if(score >= nextLevelAt && difficulty < maxDifficulty)
        {
            AdjustDifficulty();
        }

        if (!displayPlaced && highscorePositionOffset != 0)
        {
            PlaceDisplayObject();
        }


    }

    private void PlaceDisplayObject()
    {
        print("placing display object...");
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
            float previousHighscore = highScore;
            highScore = score;
            PlayerPrefsManager.SetHighscore(highScore);
            highScoreText.text = FormatFloat(highScore).ToString();
            CalculateDisplayOffset();
        }

        score = 0.0f;
        difficulty = 1;
        nextLevelAt = 100;
        scoreText.text = FormatFloat(score).ToString();
        displayPlaced = false;
    }

    void CalculateDisplayOffset()
    {
        print("Calculating new Offset...");
        float currentZPosition = player.transform.position.z;
        highscorePositionOffset = currentZPosition - player.StartingPosition.z;

        Debug.Log(string.Format("Current Z position of Player: {0} | player starting position.z: {1} | highscorePositionOffset: {2}", currentZPosition, player.StartingPosition.z, highscorePositionOffset));
        //Debug.Log(string.Format("displayPlaced: {0} | highScorePositionOffset {1}", displayPlaced, highscorePositionOffset));


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
        highScoreText.text = FormatFloat(highScore).ToString();
        highscorePositionOffset = 0;
        highscoreDisplay.SetActive(false);
    }
}
