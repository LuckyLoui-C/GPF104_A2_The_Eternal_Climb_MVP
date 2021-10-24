using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCartridge : MonoBehaviour
{
    public const float MAX_TIME = 60.0f; // Max amount of time available to play, doesn't change as can't get more than max time by killing enemies
    public AudioSource gameMusic; // Game music
    public AudioSource gameOverSound; // Game sound for game over
    public AudioSource hitSound; // Sounds for character
    public AudioSource deathSound;
    public AudioSource powerUpSound;
    public AudioSource enemySound;
    public GameObject gameOverMenu;
    public GameObject player;
    public CountdownTimer timer;
    public Text scoreText;
    public Text gameOverMessage;

    public int currentScore = 0; // Current score for this round
    public int highScore = 0; // The highest score acheived on this PC

    public bool isGameRunning; // Bool is the game scene started and currently active?


    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;
        scoreText.text = "Score: " + currentScore;
        //gameMusic.Play(); // Play the scene music
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isGameRunning) // There migh be an easier way/better way to do this! not sure
            //gameMusic.Stop();
    }

    // Function to handle adding points. Can add points when enemy is killed/collectibles etc...
    public void addPoints(int points)
    {
        currentScore += points;
        scoreText.text = "Score: " + currentScore;
    }

    public void displayGameOverMenu(string gameOverReason)
    {
        gameOverMenu.SetActive(true);
        timer.timerIsRunning = false;
        gameOverMessage.text = gameOverReason;
    }
}