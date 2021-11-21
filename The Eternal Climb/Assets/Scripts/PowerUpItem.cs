using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for the items that can be collected in each scene.
// Adjust the variables for each item accordingly
public class PowerUpItem : MonoBehaviour
{
    [Header("Power Up Settings")] // Adjust these in scene for each item type
    public float timeAdd;
    public int addHealth;
    public SpriteRenderer playerWithSword;
    public SpriteRenderer playerWithAxe;

    [Header("Speed Up Settings")]
    public float speedMultiplier; // Amount to multiply speed by
    public float duration; // How long does speed up effect last?

    private CountdownTimer countdownTimer;
    private PlayerHealth playerHealth;
    private bool collected;
    [SerializeField] private GameObject healthParticles;
    [SerializeField] private GameObject speedParticles;
    [SerializeField] private GameObject timeParticles;

    private void Start()
    {
        collected = false; // Added a bool as circle collider hitting multiple times in one collision
        playerHealth = FindObjectOfType<PlayerHealth>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
    }

    // Check if collision was with player before calling Pickup() function
    void OnTriggerEnter2D(Collider2D other) // TODO: Change in EnemyDeath script to CompareTag
    {
        if (!collected && other.CompareTag("Player"))
        {
            collected = true; // Set this to true to prevent multiple collisions with circle collider
                              //GetCompone ntInChildren<ParticleSystem>().Play();
            if (speedMultiplier > 1.0f) // If the power up is a speed multipier, use IEnumerator
                PickupSpeed(other);
            else if (this.name == "Axe")
            {
                other.GetComponent<PlayerAttack>().playerRenderer = playerWithAxe;
                other.GetComponent<PlayerAttack>().attackPoints = 2;
                Destroy(this.gameObject);
            }
            else
                Pickup(other);
        }
    }

    // Function to handle the stat changes and destroy this item
    void Pickup(Collider2D player)
    {
        // Add power up time to the timer
        //countdownTimer.timeRemaining += timeAdd;

        // If power up is health - play health particles
        // Else play time particles if time power up
        if (addHealth >= 1)
            Instantiate(healthParticles, new Vector3(transform.position.x,transform.position.y,transform.position.z - 5), transform.rotation);
        else if (timeAdd >= 1)
            Instantiate(timeParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 5), transform.rotation);

        if (playerHealth.health < 3)
        {
            // Get PlayerHealth reference from other collider
            playerHealth.health += addHealth; // Increase the player's health
        }

        Destroy(this.gameObject); // Destroy the power up.. //TODO: add sound on collect
    }    

    // Coroutine for the speed power up to have a set duration
    // before returning the speed back to normal
    void PickupSpeed(Collider2D player)
    {
        //// Adjust the player speed by power up amount
        //PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        //playerMovement.moveSpeed *= speedMultiplier;
        //
        //Instantiate(speedParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 5), transform.rotation); // Play speed power up particles
        //
        //// Item will be in the scene for duration of speed power up
        //// Disable the mesh and collider so item is not seen, and can collide again
        //GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<CircleCollider2D>().enabled = false;
        //
        //yield return new WaitForSeconds(duration); // Wait for 'duration' seconds
        //
        //playerMovement.moveSpeed /= speedMultiplier; // Return speed back to original setting

        FindObjectOfType<PlayerMovement>().nextJumpBig = true;

        Destroy(this.gameObject); // Destroy the power up.. //TODO: add sound and maybe speed up music?
    }
} 