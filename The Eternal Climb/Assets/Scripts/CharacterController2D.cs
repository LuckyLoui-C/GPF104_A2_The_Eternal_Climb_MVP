// GPF104 - A2 - Arcade Game-Milestone 1 (Prototype)
// Team Project - 'The Eternal Climb' game
// Addison Ross - 
// Brianna Essery - 
// Daniel Campbell - A00032368
// Jack Pavey - 
// Yvette Villanueva


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

    private GameCartridge gameManager; // Game cartridge (manager) referenced in the scene

    public Vector2 movementDirection; // Direction the player is moving towards

    public int playerHealth; // Health remaining, int for simple health bar
    public bool playerIsAlive;
    public bool playerCanMove;
    public float movementSpeed;
    public float rotationDirection; // Direction the player is facing
    public string playerName;

    // Assign variables before starting game
    public void Awake()
    {
        playerIsAlive = true;
        playerCanMove = false;
        rotationDirection = 0.0f;
        movementSpeed = 0.0f;
        movementDirection = Vector2.right;
    }



    // Start is called before the first frame update
    void Start()
    {
        playerCanMove = true;
        gameManager = GameObject.Find("GameCartridge").GetComponent<GameCartridge>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Collision - either from weapon or if player touches enemy or spikes or something
    // Player collides with "Health" or "Power Up" also handled here?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);

    }
}
