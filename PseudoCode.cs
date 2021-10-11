// Pseudo  code for c#

public class PlayerMovement : MonoBehaviour 
{
    public CharacterController2D controller;
    public float runSpeed = 40.0f;

    float horizontalMove = 0.0f;

    void start () 
    {

    }

    void update ()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }

    void FixedUpdate ()
    {
        // Move the player character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

}