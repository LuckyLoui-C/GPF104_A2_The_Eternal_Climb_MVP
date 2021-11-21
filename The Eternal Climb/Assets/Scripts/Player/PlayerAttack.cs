using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public KeyCode attackKey;
    public float pauseBefore;
    public float cooldownAfter;
    public float hitmarkerTime;
    public int attackPoints;
    private bool isAttacking;
    //public float damage; Will be used when enemies take more than 1 hit to die

    [Header("Attack Hitbox Settings")]
    public LayerMask enemyLayerMask; 
    public float hitboxPosX; // The x position of the jump OverlapBox
    public float hitboxPosY; // The y position of the jump OverlapBox
    public float hitboxHeight; // The height of the jump OverlapBox
    public float hitboxWidth; // The width of the jump OverlapBox

    [Header("Component References")]
    public SpriteRenderer playerRenderer;
    public Animator animator;
    public AudioClip attackSnd;

    private void Start()
    {
        isAttacking = false;
        attackPoints = 1;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        if(Input.GetAxisRaw("Horizontal") == -1 && Mathf.Sign(hitboxPosX) == 1)
        {
            hitboxPosX *= -1;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && Mathf.Sign(hitboxPosX) == -1)
        {
            hitboxPosX *= -1;
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        animator.Play("CharacterAttack");
        yield return new WaitForSeconds(pauseBefore);
        Collider2D collider = Physics2D.OverlapBox(new Vector2(this.transform.position.x + hitboxPosX, this.transform.position.y + hitboxPosY), new Vector2(hitboxWidth, hitboxHeight), 0f, enemyLayerMask);
        Color originalColor = playerRenderer.color;
        Instantiate(attackSnd, Vector3.zero, Quaternion.identity);
        playerRenderer.color = Color.red;
        if(collider != null)
        {
            collider.GetComponent<EnemyBehaviour>().Die(attackPoints);
        }
        yield return new WaitForSeconds(hitmarkerTime);
        playerRenderer.color = originalColor;
        yield return new WaitForSeconds(cooldownAfter - hitmarkerTime);
        isAttacking = false;
        animator.Play("CharacterIdle");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(this.transform.position.x + hitboxPosX, this.transform.position.y + hitboxPosY), new Vector2(hitboxWidth, hitboxHeight));
    }
}
