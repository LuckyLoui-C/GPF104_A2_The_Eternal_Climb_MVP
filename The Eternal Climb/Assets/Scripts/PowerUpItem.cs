using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for the items that can be collected in each scene.
// Adjust the variables for each item accordingly
public class PowerUpItem : MonoBehaviour
{
    [Header("Component References")]
    public CountdownTimer countdownTimer;
    //TODO: add PlayerHealth reference

    [Header("Power Up Settings")] // Adjust these in scene for each item type
    public float speedMultiplier;
    public float timeAdd;
    public int addHealth;

    private bool collected = false; // Added a bool as circle collider hitting multiple times in one collision

    // Check if collision was with player before calling Pickup() function
    void OnTriggerEnter2D(Collider2D other) // TODO: Change in EnemyDeath script to CompareTag
    {
        if (!collected && other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + "was picked up item");
            collected = true; // Set this to true to prevent multiple collisions with circle collider
            Pickup(other);
        }
    }

    // Function to handle the stat changes and destroy this item
    void Pickup(Collider2D player)
    {
        // Adjust the player speed by power up amount
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.moveSpeed *= speedMultiplier;

        // Add power up time to the timer
        countdownTimer.timeRemaining += timeAdd;
        
        Debug.Log("Stats changed");
        Destroy(this.gameObject); // Destroy the power up.. //TODO: add sound on collect
    }
} 