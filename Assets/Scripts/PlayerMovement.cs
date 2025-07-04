using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private GameParameters _GameParameters;
    
    Vector3 velocity;
 
    bool isGrounded;


    private void Awake()
    {
        _GameParameters = GameManager.Instance.GameParameters;
    }
    
    // Update is called once per frame
    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        //right is the red Axis, foward is the blue axis
        Vector3 moveAmount = transform.right * x + transform.forward * z;
 
        controller.Move(moveAmount * _GameParameters.PlayerParameters.MovementSpeed * Time.deltaTime);
 
        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity = _GameParameters.PlayerParameters.JumpHeight * -2f * _GameParameters.WorldParameters.Gravity;
            velocity.x = Mathf.Sqrt(velocity.x);
            velocity.y = Mathf.Sqrt(velocity.y);
            velocity.z = Mathf.Sqrt(velocity.z);
        }
 
        velocity += _GameParameters.WorldParameters.Gravity * 2f * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);
    }
}
