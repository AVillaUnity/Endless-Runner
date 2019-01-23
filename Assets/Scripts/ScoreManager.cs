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
    private PlayerMovement playerMovement;
    private Player player;
    private GameManager gameManager;
    private AudioManager audioManager;
    private SpawnFireworks fireworksSpawner;
    private float highscorePositionOffset;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.onReset += ResetTimer;

        audioManager = AudioManager.instance;

        highScore = PlayerPrefsManager.GetHighscore();
        UdpateHighScoreText();
        scoreText.text = FormatFloat(score).ToString();
        loseScoreText.text = scoreText.text;

        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        player = GameObject.FindObjectOfType<Player>();
        fireworksSpawner = highscoreDisplay.GetComponent<SpawnFireworks>();

        GetInitialOffset();
        //ResetHighScore();
        highscoreDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerMovement.PlayerMoving){ return; }
        if (player.IsDead) { return; }

        score += playerMovement.DistanceTraveled;

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

        if(score > highScore && !fireworksSpawner.SpawnedFireworks && displayPlaced)
        {
            fireworksSpawner.Spawn();
            audioManager.Play("Yay");
        }


    }

    private void PlaceDisplayObject()
    {
        Vector3 newPosition = highscoreDisplay.transform.position;
        newPosition.z = playerMovement.transform.position.z + highscorePositionOffset;
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
        playerMovement.IncrementSpeed();
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
        float currentZPosition = playerMovement.transform.position.z;
        highscorePositionOffset = currentZPosition - playerMovement.StartingPosition.z;

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
