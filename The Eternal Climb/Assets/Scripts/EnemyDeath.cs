using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private BoxCollider2D enemyCollider; // Prevent collisions after enemy death
    private SpriteRenderer spriteRenderer; // Change colour of sprite

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            enemyCollider.enabled = false; // No longer collides with player (or weapon)
            spriteRenderer.color = Color.black; // Signify death
            enemyRb.gravityScale = 3.0f; // Allow dead enemy to fall off screen
        }
    }
}