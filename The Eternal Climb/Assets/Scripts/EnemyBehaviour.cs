using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Component References")]
    private PlayerHealth playerHealth;
    private Rigidbody2D enemyRb;
    private BoxCollider2D enemyCollider; // Prevent collisions after enemy death
    private SpriteRenderer spriteRenderer; // Change colour of sprite
    public GameCartridge gameManager;

    [Header("Attack Settings")]
    public float damage;
    public float attackCooldown;

    private bool isAttacking;
    private bool canAttack;

    private void Start()
    {
        // Initialization of references to components
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyRb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialization of variables
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy hasn't attack recently and is touching the player then attack
        if (collision.CompareTag("Player") && !isAttacking && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        // Perform the attack before waiting for the cooldown
        isAttacking = true;
        playerHealth.DoDamage(damage);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    public void Die()
    {
        canAttack = false;
        enemyCollider.enabled = false; // No longer collides with player (or weapon)
        spriteRenderer.color = Color.black; // Signify death
        enemyRb.gravityScale = 3.0f; // Allow dead enemy to fall off screen
        gameManager.addPoints(1); // Add a point for that sweet kill
    }
}