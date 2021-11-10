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
    public Animator animator;

    [Header("Health Settings")]
    public float health;

    public float animationWait;

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
        //StartCoroutine(DamageAnimation());
        // TODO: Give the player a push away from the enemy that hit it
        health -= amountOfDamage;
        animator.Play("CharacterDamage");
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

/*    IEnumerator DamageAnimation()
    {
        animator.Play("CharacterDamage");
        Debug.Log("Damage Anim. play");
        yield return new WaitForSeconds(animationWait);
        animator.Play("CharacterIdle");
        Debug.Log("Damage Anim. play");
    }*/
}
