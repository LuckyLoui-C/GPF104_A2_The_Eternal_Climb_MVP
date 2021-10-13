using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    [SerializeField] private float moveSpeed; // Enemy's speed
    private float xDirection;
    private float movement;
    private bool faceRight = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        xDirection = -1.0f;
        moveSpeed = 2.0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Wall>()) // If he hit that wall, spin around and walk the other way
        {
            xDirection *= -1.0f;
        }
    }
    private void Update()
    {
        movement = xDirection * moveSpeed;
        enemyRb.velocity = new Vector2(movement, enemyRb.velocity.y);
    }
}
