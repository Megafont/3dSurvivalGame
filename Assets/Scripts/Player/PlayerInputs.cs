using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SurvivalGame.Player
{
    public class PlayerInputs : MonoBehaviour
    {
        public static PlayerInputs Instance { get; private set; }


        private PlayerInput _PlayerInput;

        [SerializeField]
        private InputActionAsset _NormalInputActions;
        [SerializeField]
        private InputActionAsset _VrInputActions;

        
        private InputActionMap _ActionMap_VR_LeftInteraction;
        private InputActionMap _ActionMap_VR_RightInteraction;
        
        // Input Actions
        private InputAction _PickupAction;
        private InputAction _PickupAction_VR_LeftHand;
        private InputAction _PickupAction_VR_RightHand;
        
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
        
            Instance = this;
            
            //_PlayerInput = (PlayerInput) FindAnyObjectByType(typeof(PlayerInput));
            
            _ActionMap_VR_LeftInteraction = _VrInputActions.FindActionMap("XRI Left Interaction");
            _ActionMap_VR_RightInteraction = _VrInputActions.FindActionMap("XRI Right Interaction");

            InitializeInputActions();
        }

        private void InitializeInputActions()
        {
            _PickupAction = _NormalInputActions.FindAction("Interact");
            _PickupAction_VR_LeftHand = _ActionMap_VR_LeftInteraction.FindAction("Select");
            _PickupAction_VR_RightHand = _ActionMap_VR_RightInteraction.FindAction("Select");
        }

        private void Update()
        {
            Pickup = _PickupAction.WasReleasedThisFrame();
            Pickup_VR_LeftHand = _PickupAction_VR_LeftHand.WasReleasedThisFrame();
            Pickup_VR_RightHand = _PickupAction_VR_RightHand.WasReleasedThisFrame();
        }

        
        public bool Pickup { get; private set; }
        public bool Pickup_VR_LeftHand { get; private set; }
        public bool Pickup_VR_RightHand { get; private set; }
    }
}