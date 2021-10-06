using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCartridge : MonoBehaviour
{
    public const float REMAINING_TIME = 240.0f; // Max amount of time available to play,
                                                // doesn't change as can't get more than max 
                                                // time from power ups
    public float gameTimeRemaining = 240.0f; // The current time remaining (will countdown)

    // private ObjectSpawner objectSpawner // For spawning of enemies, power ups, items? health?
    //                                     // May be easier to just hard code where these spawn
    // in the scene/in their own cs script

    /*    public AudioSource gameMusic; // Game music
        public AudioSource gameOverSound; // Game sound for game over
        public AudioSource hitSound; // Sounds for character
        public AudioSource deathSound;
        public AudioSource powerUpSound;
        public AudioSource enemySound;*/

    public Text scoreText;
    public Text timerText;

    public int currentScore = 0; // Current score for this round
    //public int highScore = 0; // The highest score acheived on this PC

    public bool isGameRunning; // Bool is the game scene started and currently active?


    // Start is called before the first frame update
    void Start()
    {
        isGameRunning = true;
        scoreText.text = "Score: " + currentScore;
        //gameMusic.Play(); // Play the scene music
        //objectSpawner = GameObject.Find("ObjectSpawner").GetComponent<ObjectSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameRunning) // There migh be an easier way/better way to do this! not sure
        {
            //gameMusic.Stop();
            //Destroy(objectSpawner); // Stop objects from spawning
        }
    }
    // Function to handle adding points. Can add points when enemy is killed/collectibles etc...
    public void AddPoints(int points)
    {
        currentScore += points;
        scoreText.text = "Score: " + currentScore;
    }
}
