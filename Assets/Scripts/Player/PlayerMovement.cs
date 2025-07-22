using UnityEngine;

using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Systems;


namespace SurvivalGame.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        
        private ParameterData<Vector3> _Gravity;
        private ParameterData<float> _MovementSpeed;
        private ParameterData<float> _JumpHeight;
        private ParameterData<bool> _EnableVrMode;
        
        Vector3 velocity;

        bool isGrounded;


        private void Awake()
        {
            
        }

        private void Start()
        {
            _Gravity = GameManager.Instance.GameParameters.GetParameterData<Vector3>(ParameterIDs.WORLD_GRAVITY);
            _MovementSpeed = GameManager.Instance.GameParameters.GetParameterData<float>(ParameterIDs.PLAYER_MOVEMENT_SPEED);
            _JumpHeight = GameManager.Instance.GameParameters.GetParameterData<float>(ParameterIDs.PLAYER_MOVEMENT_JUMP_HEIGHT);
            _EnableVrMode = GameManager.Instance.GameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_ENABLE_VR_MODE);    
        }
        
        // Update is called once per frame
        void Update()
        {
            // Check if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Right is the red Axis, foward is the blue axis
            Vector3 moveAmount = transform.right * x + transform.forward * z;

            
            if (!((bool) _EnableVrMode.Value))
                controller.Move(moveAmount * (_MovementSpeed.Value * Time.deltaTime));
            

            // Check if the player is on the ground so he can jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                // The equation for jumping
                velocity = _JumpHeight.Value * -2f *
                           _Gravity.Value;
                velocity.x = Mathf.Sqrt(velocity.x);
                velocity.y = Mathf.Sqrt(velocity.y);
                velocity.z = Mathf.Sqrt(velocity.z);
            }


            // Apply gravity.
            velocity += _Gravity.Value * (2f * Time.deltaTime);
            
            // Apply the results of jumping and gravity to the player controller.
            controller.Move(velocity * Time.deltaTime);
        }
    }
    
}