using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;

    public Transform groundPointA;
    public Transform groundPointB;
    public Transform wallPointHolder;
    public Transform wallPointA;
    public Transform wallPointB;

    public float moveSpeed;
    public float jumpForce;

    private int currentJumpNum;
    public int maxJumpNum;

    private bool jumpAxisInUse;
    private bool isGrounded;
    private bool isTouchingWall;


    private void Awake()
    {
        // Initialization of references to components
        playerRb = this.GetComponent<Rigidbody2D>();

        // Initialization of variables
    }
    private void Update()
    {
        // Horizontal input and movement
        float movement = Input.GetAxisRaw("Horizontal") * moveSpeed;
        playerRb.velocity = new Vector3(movement, playerRb.velocity.y,0);

        if(Input.GetAxisRaw("Horizontal") == -1)
        {
            wallPointHolder.rotation = Quaternion.Euler(0,0,0);
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            wallPointHolder.rotation = Quaternion.Euler(0, 0, 180);
        }

        // Convert the constant hit register of GetAxis to a one hit register and call a jump on register
        if (Input.GetAxisRaw("Jump") != 0)
        {
            if (jumpAxisInUse == false)
            {
                if(currentJumpNum > 0)
                {
                    playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, 0);
                    currentJumpNum--;
                }
                jumpAxisInUse = true;
            }
        }
        if (Input.GetAxisRaw("Jump") == 0)
        {
            jumpAxisInUse = false;
        }

        // Check if touching the things
        isGrounded = Physics2D.OverlapArea(groundPointA.position, groundPointB.position, groundLayerMask);
        isTouchingWall = Physics2D.OverlapArea(wallPointA.position, wallPointB.position, wallLayerMask);

        // Reset the jumps every time the player is grounded
        if (isGrounded && currentJumpNum != maxJumpNum)
        {
            currentJumpNum = maxJumpNum;
        }


    }
}
