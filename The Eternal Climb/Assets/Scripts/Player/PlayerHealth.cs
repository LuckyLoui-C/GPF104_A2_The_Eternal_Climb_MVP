using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Component References")]
    // These are temporary for the player death anim
    private SpriteRenderer playerRenderer;
    private Rigidbody2D playeRb;
    private Collider2D playerCollider;
    private GameCartridge gameManager;
    public Collider2D otherPlayerCollider;

    [Header("Health Settings")]
    public float health;

    private void Start()
    {
        playeRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameCartridge>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            Die("You got hit 1 too many times!");
        }
    }
    public void DoDamage(float amountOfDamage)
    {
        // TODO: Give the player a push away from the enemy that hit it
        health -= amountOfDamage;
    }
    public void Die(string deathReason)
    {
        // Temporary death animation
        playerCollider.enabled = false;
        otherPlayerCollider.enabled = false;
        playerRenderer.color = Color.black;
        playeRb.gravityScale = 3.0f;
        //TODO: gameManager.deathSound.Play();
        gameManager.displayGameOverMenu(deathReason);
    }
}
