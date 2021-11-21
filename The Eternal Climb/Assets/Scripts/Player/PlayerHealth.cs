using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Component References")]
    // These are temporary for the player death anim
    private SpriteRenderer playerRenderer;
    private Rigidbody2D playerRb;
    private Collider2D playerCollider;
    private GameCartridge gameManager;
    public Collider2D otherPlayerCollider;
    public Animator animator;

    [Header("Health Settings")]
    public float health;

    public float animationWait;
    private bool isDead;
    public float knockbackForce;

    private void Start()
    {
        isDead = false;
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameCartridge>();
    }
    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            string deathString = "";
            switch (Random.Range(0,6))
            {
                case 0:
                    deathString = string.Format("\"{0}\"", "You got hit 1 too many times");
                    break;
                case 1:
                    deathString = string.Format("\"{0}\"", "You got beaten to a pulp");
                    break;
                case 2:
                    deathString = string.Format("\"{0}\"", "You were decapitated");
                    break;
                case 3:
                    deathString = string.Format("\"{0}\"", "Just don't get hit");
                    break;
                case 4:
                    deathString = string.Format("\"{0}\"", "You're not supposed to get hit");
                    break;
                case 5:
                    deathString = string.Format("\"{0}\"", "You suffered from fatal head trauma");
                    break;
            }
            Die(deathString);
        }
    }
    public void DoDamage(float amountOfDamage, Vector3 hitterPos)
    {
        //StartCoroutine(DamageAnimation());
        Vector3 dir = hitterPos - transform.position;
        dir = -dir.normalized;
        playerRb.AddForce(dir * knockbackForce);
        health -= amountOfDamage;
        animator.Play("CharacterDamage");
        gameManager.takeDamageSound.Play();
    }
    public void Die(string deathReason)
    {
        isDead = true;
        // Temporary death animation
        playerCollider.enabled = false;
        otherPlayerCollider.enabled = false;
        playerRenderer.color = Color.black;
        playerRb.gravityScale = 3.0f;
        //TODO: gameManager.deathSound.Play();
        gameManager.displayGameOverMenu(deathReason);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerFallDeath"))
        {
            string deathString = "";
            switch (Random.Range(0,7))
            {
                case 0:
                    deathString = string.Format("\"{0}\"", "You fell into the void");
                    break;
                case 1:
                    deathString = string.Format("\"{0}\"", "The Eternal Fall");
                    break;
                case 2:
                    deathString = string.Format("\"{0}\"", "You really missed that jump?");
                    break;
                case 3:
                    deathString = string.Format("\"{0}\"", "You fell a really long way");
                    break;
                case 4:
                    deathString = string.Format("\"{0}\"", "Eternal Climber #" + Random.Range(1000,10000) + " tripped and died" );
                    break;
                case 5:
                    deathString = string.Format("\"{0}\"", "You decided you couldn't take it anymore");
                    break;
                case 6:
                    deathString = string.Format("\"{0}\"", "Eternal Climber #" + Random.Range(1000, 10000) + " forgot to jump");
                    break;
            }
            Die(deathString);
        }
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
