using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCartridge : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource gameMusic; // Game music
    public AudioSource gameOverSound; // Game sound for game over
    public AudioSource hitSound; // Sounds for character
    public AudioSource deathSound;
    public AudioSource powerUpSound;
    public AudioSource enemySound;

    [Header("Component References")]
    public GameObject player;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public Text gameOverMessage;
    public Text scoreText;
    public CountdownTimer timer;


    public const float MAX_TIME = 60.0f; // Max amount of time available to play, doesn't change as can't get more than max time by killing enemies
    public int currentScore = 0; // Current score for this round
    public int highScore = 0; // The highest score acheived on this PC

    public bool isGameRunning; // Bool is the game scene started and currently active?
    public bool paused = false; // Bool to pause and unpause the game


    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        timer = GameObject.Find("TimerText").GetComponent<CountdownTimer>();
        player = GameObject.Find("Player").GetComponent<GameObject>();
        gameOverMenu = GameObject.Find("GameOverMenu").GetComponent<GameObject>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<GameObject>();
        gameOverMessage = GameObject.Find("GameOverReasonText").GetComponent<Text>();
}

    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;
        scoreText.text = "Score: " + currentScore;
        //TODO: gameMusic.Play(); // Play the scene music
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        //if (!isGameRunning) // TODO:
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
        isGameRunning = false;
        gameOverMenu.SetActive(true);
        timer.timerIsRunning = false;
        gameOverMessage.text = gameOverReason;
    }

    // For use with the resume button in pause menu scene
    public void unPause()
    {
        Time.timeScale = 1f;
    }
}