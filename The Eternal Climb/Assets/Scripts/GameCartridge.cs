using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCartridge : MonoBehaviour
{
    [Header("Music Sources")]
    public AudioSource gameMusic; // Game music
    public AudioSource gameOverMusic; // Game sound for game over

    [Header("Character Sounds")]
    public AudioSource hitSound; // Sounds for character attack
    public AudioSource takeDamageSound; // Sounds for character attack
    public AudioSource walkingSound; // Character walking sound
    public AudioSource deathSound;

    [Header("Other Game Sounds")]
    public AudioSource powerUpSound;
    public AudioSource enemySound;

    [Header("Component References")]
    public GameObject player;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public TextMeshProUGUI gameOverMessage;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI distanceScoreText;
    public CountdownTimer timer; // Timer to add to time score


    public const float MAX_TIME = 60.0f; // Max amount of time available to play, doesn't change as can't get more than max time by killing enemies
    public int currentScore = 0; // Current score for this round
    public int highScore = 0; // The highest score acheived on this PC

    public bool isGameRunning; // Bool is the game scene started and currently active?
    public bool paused = false; // Bool to pause and unpause the game


    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;
        scoreText.text = currentScore.ToString();
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
          //  gameMusic.Stop();
    }

    // Function to handle adding points. Can add points when enemy is killed/collectibles etc...
    public void addPoints(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }

    public void displayGameOverMenu(string gameOverReason)
    {
        isGameRunning = false;
        gameOverMenu.SetActive(true);
        //timer.timerIsRunning = false; // not needed in new game mode
        gameOverMessage.text = gameOverReason;
        finalScoreText.text = scoreText.text;
        finalDistanceText.text = distanceScoreText.text;
    }

    // For use with the resume button in pause menu scene
    public void unPause()
    {
        Time.timeScale = 1f;
    }
}