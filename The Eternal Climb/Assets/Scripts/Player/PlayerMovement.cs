using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component References")]
    private Rigidbody2D playerRb;
    public SpriteRenderer playerRenderer;
    public Animator animator;
    private Transform playerTransform;

    [Header("Movement Settings")]
    public float moveSpeed; // The players moving speed which is set in the inspector

    [Header("Standard Jumping Settings")]
    public LayerMask groundLayerMask; // A layer of objects that are classed as the ground (the player can't jump if standing on something not in this layer)

    public float jumpForce; // How strong the force the force applied when jumping should be
    public float jumpBuffer; // How long after the jump key is pressed should it register a jump midair
    public float fallMultiplier; // How quickly the player should fall
    public float smallJumpModifier; // The stronger this variable, the smaller a jump when the user just taps the jump key
    public float jumpBoxPosY; // The y position of the jump OverlapBox
    public float jumpBoxHeight; // The height of the jump OverlapBox
    public float jumpBoxWidth; // The width of the jump OverlapBox

    public int maxJumpNum; // How many air jumps the player is set to have (cannot be zero)
    public int currentJumpNum; // How many air jumps the player has left

    public float animWait; // Set the time waiting for the animator to set back to idle

    private bool jumpOnGrounded;
    private bool jumpRequested; // Has the player pushed the jump input
    private bool jumpAxisInUse; // This variable is used to convert a constant input to a toggle input
    private bool isGrounded; // Is the player touching the ground


    [Header("Wall Jumping Settings")]
    public LayerMask wallLayerMask; // A layer of objects that the player can wall jump off of and slide down (the player can't interact with a wall not in this layer)

    public float wallSlideSpeed; // How slowly does the player slide down a wall

    public float wallBoxPosY; // The y position of the wall OverlapBox
    public float wallBoxHeight; // The height of the wall OverlapBox
    public float wallBoxWidth; // The width of the wall OverlapBox

    private bool isTouchingWall; // Is the player touching a wall

    private void Awake()
    {
        // Initialization of references to components
        playerRb = this.GetComponent<Rigidbody2D>();
        playerTransform = this.GetComponent<Transform>();
    }

    private void Update()
    {

        // Horizontal input and movement
        if (Input.GetAxisRaw("Horizontal") == -1 && Mathf.Sign(playerTransform.localScale.x) == -1)
            playerTransform.localScale = new Vector3(playerTransform.localScale.x * -1, transform.localScale.y, 0);
        if (Input.GetAxisRaw("Horizontal") == 1 && Mathf.Sign(playerTransform.localScale.x) == 1)
            playerTransform.localScale = new Vector3(playerTransform.localScale.x * -1, transform.localScale.y, 0);

        float movement = Input.GetAxisRaw("Horizontal") * moveSpeed;
        playerRb.velocity = new Vector3(movement, playerRb.velocity.y, 0);

        // Converts the constant input of GetAxisRaw to a toggle and requests a jump (which is received in FixedUpdate())
        if (Input.GetAxisRaw("Jump") != 0)
        {
            if (jumpAxisInUse == false)
            {
                jumpAxisInUse = true;
                if(!isGrounded && currentJumpNum <= 0)
                {
                    StartCoroutine(DelayedJump());
                }
                else if (currentJumpNum > 0)
                {
                    jumpRequested = true;
                }
            }
        }
        if (Input.GetAxisRaw("Jump") == 0)
        {
            jumpAxisInUse = false;
        }

        // Creates an invisible box that detects if its touching the ground or wall respectively and returns true if it is
        isGrounded = Physics2D.OverlapBox(new Vector2(this.transform.position.x, this.transform.position.y - jumpBoxPosY), new Vector2 (jumpBoxWidth,jumpBoxHeight),0f,groundLayerMask);
        isTouchingWall = Physics2D.OverlapBox(new Vector2(this.transform.position.x, this.transform.position.y - wallBoxPosY), new Vector2(wallBoxWidth, wallBoxHeight), 0f, wallLayerMask);

        // Resets the amount of jumps if the player is on the ground
        if (isGrounded && currentJumpNum != maxJumpNum)
        {
            currentJumpNum = maxJumpNum;
        }

        // Jump if the jump key was pushed while not grounded and the player became grounded within the buffer time
        if (jumpOnGrounded && isGrounded)
        {
            currentJumpNum = maxJumpNum;
            jumpRequested = true;
        }
    }

    private void OnDrawGizmos()
    {
        // Draws a wireframe cube in place of the invisible Physics2D collision checks so that we can see their location and size in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(this.transform.position.x, this.transform.position.y - jumpBoxPosY), new Vector2(jumpBoxWidth, jumpBoxHeight));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(this.transform.position.x, this.transform.position.y - wallBoxPosY), new Vector2(wallBoxWidth, wallBoxHeight));
    }

    private void FixedUpdate()
    {
        // All physics are handled inside of FixedUpdate
        // Jump: Resets the y velocity of the player before launching them into the air
        if(jumpRequested)
        {
            if(!isGrounded && !jumpOnGrounded) 
                currentJumpNum--;
            jumpOnGrounded = false;
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, 0);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = false;
            StartCoroutine(MakeHimJump());
        }

        // If the player is moving into a wall midair, they slide down the wall slower
        if (isTouchingWall && Input.GetAxisRaw("Horizontal") != 0 && playerRb.velocity.y < 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x,0);
            playerRb.gravityScale = wallSlideSpeed;
        }
        // A gravity modifier is applied to the player if they are falling, or if they want to perform a smaller jump by only pressing the jump button quickly
        else
        {
            if (playerRb.velocity.y < 0)
            {
                playerRb.gravityScale = fallMultiplier;
            }
            else if (playerRb.velocity.y > 0 && Input.GetAxisRaw("Jump") != 1)
            {
                playerRb.gravityScale = smallJumpModifier;
            }
            else
            {
                playerRb.gravityScale = 1;
            }
        }
    }

    // Allows for the jump button to be pressed while not grounded and still jump when grounded within a small period of time
    // (Purely cause it makes the player feel easier to control)
    IEnumerator DelayedJump()
    {
        jumpOnGrounded = true;
        yield return new WaitForSeconds(jumpBuffer);
        jumpOnGrounded = false;
    }

    IEnumerator MakeHimJump()
    {
        animator.Play("CharacterJump");
        yield return new WaitForSeconds(animWait);
        animator.Play("CharacterIdle");
    }
}