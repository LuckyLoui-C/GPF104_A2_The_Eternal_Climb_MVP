using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bombs for dragon to drop, can be used for long-distance enemy attacks
public class Projectile : MonoBehaviour
{
    public float selfDestroyTime; // Time that this object will be on the screen for
    public int damage; // Amount of HP the attack doest to the enemy

    private bool hit;

    private void Start()
    {
        selfDestroyTime = 5;
        damage = 1;
        hit = false;
    }

    private void Update()
    {
        selfDestroyTime -= Time.deltaTime;
        if (selfDestroyTime <= 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hit)
        {
            hit = true;
            other.GetComponent<PlayerHealth>().DoDamage(damage,transform.position); // Decrease the player's health
            Destroy(this.gameObject);
        }
    }
}
