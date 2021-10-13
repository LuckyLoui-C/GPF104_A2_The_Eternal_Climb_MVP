using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private BoxCollider2D enemyCollider;
    private SpriteRenderer spriteRenderer;

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
            enemyCollider.enabled = false;
            spriteRenderer.color = Color.green;
            enemyRb.gravityScale = 2.0f;
        }
    }


}
