using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private float score = 0.0f;
    private int difficulty = 1;
    private int maxDifficulty = 10;
    private int nextLevelAt = 10;
    private PlayerMovement player;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        scoreText.text = FormatFloat(score).ToString();
        player = GameObject.FindObjectOfType<PlayerMovement>();
        player.onDeath += ResetTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.GameStarted) { return; }

        score += player.DistanceTraveled * difficulty;
        scoreText.text = FormatFloat(score).ToString();

        if(score >= nextLevelAt && difficulty < maxDifficulty)
        {
            AdjustDifficulty();
        }
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
        score = 0.0f;
        scoreText.text = FormatFloat(score).ToString();

    }
}
