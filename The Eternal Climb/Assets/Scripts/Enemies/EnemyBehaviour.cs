using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Component References")]
    public Animator animator;
    public Projectile bomb;
    public Projectile bullet;
    private GameCartridge gameManager;
    private PlayerHealth playerHealth;
    private Rigidbody2D enemyRb;
    private BoxCollider2D enemyCollider; // Prevent collisions after enemy death
    private SpriteRenderer spriteRenderer; // Change colour of sprite

    [Header("Attack Settings")]
    public float damage;
    public float attackCooldown;
    public float timeReward;

    private bool isAttacking;
    private bool canAttack;
    private float shootCountdown;

    public LayerMask groundLayerMask;

    private void Start()
    {
        // Initialization of references to components
        playerHealth = FindObjectOfType<PlayerHealth>();
        gameManager = FindObjectOfType<GameCartridge>();
        enemyRb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialization of variables
        canAttack = true;
        shootCountdown = 2.0f;
        if(this.CompareTag("Dragon"))
        {
            if(Physics2D.OverlapBox(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(1, 1), 0f, groundLayerMask))
            {
                Destroy(this.gameObject);
                return;
            }
        }
    }

    private void Update()
    {
        shootCountdown -= Time.deltaTime;
        // If this enemy is type dragon, attack will be drop bombs periodically
        if (this.CompareTag("Dragon") && shootCountdown <= 0.0f)
        {
            DropBombs();
            shootCountdown = 5.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy hasn't attack recently and is touching the player then attack
        if (collision.CompareTag("Player") && !isAttacking && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    // Basic enemy attack function
    IEnumerator Attack()
    {
        // Perform the attack before waiting for the cooldown
        if (!this.CompareTag("Dragon"))
                    animator.Play("SkeletonAttack");
        isAttacking = true;
        playerHealth.DoDamage(damage,transform.position);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        if (!this.CompareTag("Dragon"))
            animator.Play("SkeletonWalkCycle");
    }

    // After dropBombCountdown time, drragon drops another bomb
    public void DropBombs()
    {
        Projectile bombClone = Instantiate(bomb, transform.position, transform.rotation);

        bomb.selfDestroyTime = 5;
        //Instantiate(dropItemPrefab, this.GetComponent<Transform>().transform); // Instantiate the bomb at the location of the dragon enemy
        
    }

    public void Die()
    {
        canAttack = false;
        enemyCollider.enabled = false; // No longer collides with player (or weapon)
        spriteRenderer.color = Color.black; // Signify death
        enemyRb.gravityScale = 3.0f; // Allow dead enemy to fall off screen
        gameManager.addPoints(1); // Add a point for that sweet kill
        //gameManager.timer.timeRemaining += timeReward;
    }
}