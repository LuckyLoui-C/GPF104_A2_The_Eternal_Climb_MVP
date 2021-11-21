using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creates a countdown timer
public class CountdownTimer : MonoBehaviour
{
    public float timeScore; // Set in scene // Not needed in new game mode
    public bool timerIsRunning = false; // Can pause the timer from GameCartridge if we want (i.e. collectible that pauses time for 3 sec etc.)
    public Text timeText; // to display on UI
    public GameCartridge gameManager;

    private void Start()
    {
        timerIsRunning = true; // Start the timer
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (gameManager.isGameRunning)
            {
                timeScore += Time.deltaTime; // Every frame, minus delatTime from remaining time, counts down.
                DisplayTime(timeScore);
            }
            else
            {
                Debug.Log("Time ran out");
                //timeRemaining = 0;
                //gameManager.player.GetComponent<PlayerHealth>().Die("Time ran out! :(");
                timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay) // TODO: Timer UI
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // Divide by 60 to get min. remaining
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // Modulus 60 to get sec. remaining

        timeText.text = string.Format("Time Score: {0:00}:{1:00}", minutes, seconds);
    }
}