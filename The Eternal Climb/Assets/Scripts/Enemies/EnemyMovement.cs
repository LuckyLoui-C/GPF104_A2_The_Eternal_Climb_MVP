using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    public float moveSpeed; // Enemy's speed
    private float xDirection; // Left or right movement direction
    private float movement; // 

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        xDirection = -1.0f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlatformEdge") || other.CompareTag("Wall")) // If he hit that wall, spin around and walk the other way
        {
            xDirection *= -1.0f;
            if (xDirection < 0.0f)
            {
                GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0); // Turn the enemy to face the other way
            }
            else
            {
                GetComponent<Transform>().rotation = new Quaternion(0, -180, 0, 0);
            }
        }
    }
    private void Update()
    {
        movement = xDirection * moveSpeed;
        enemyRb.velocity = new Vector2(movement, enemyRb.velocity.y);
    }
}
