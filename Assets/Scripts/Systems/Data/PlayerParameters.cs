using UnityEngine;
using UnityEngine.Serialization;

namespace SurvivalGame.Systems.Data
{
    [CreateAssetMenu(fileName = "PlayerParams", menuName = "Scriptable Objects/PlayerParams")]
    public class PlayerParameters : ScriptableObject
    {
        [Header("Character Controller Parameters")]

        public float SlopeLimit = 45f;

        public float StepOffset = 0.3f;
        public float SkinWidth = 0.08f;
        public float MinMoveDistance = 0.001f;
        public Vector3 Center = Vector3.zero;
        public float Radius = 0.6f;
        public float Height = 1.7f;


        [Header("Movement Parameters")]
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [Tooltip("The max player movement speed when playing in the normal mode.")]
        public float MovementSpeed = 12f;
        [Tooltip("The max player turning speed in degrees per second when playing in normal mode.")]
        public float TurnSpeed = 60f;
        [Tooltip("The max jump height when playing in normal mode.")]
        public float JumpHeight = 2f;

        [FormerlySerializedAs("VrMovementSpeed")]
        [Space(10)]
        
        [Tooltip("The max player movement speed when playing in VR mode.")]
        public float MovementSpeed_VR = 12f;
        [FormerlySerializedAs("VrTurnSpeed")] [FormerlySerializedAs("VrContinuousTurnSpeed")] [Tooltip("The max player turning speed in degrees per second when playing in VR mode.")]
        public float TurnSpeed_VR = 60f;
        [FormerlySerializedAs("VrSnapTurnAmount")] [Tooltip("The number of degrees to turn when using snap turning in VR mode.")]
        public float SnapTurnAmount_VR = 45f;
        [FormerlySerializedAs("VrJumpHeight")] [Tooltip("The max jump height when playing in VR mode.")]
        public float JumpHeight_VR = 2f;        

        
        [Header("Gameplay Parameters")] 
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        [Tooltip("The max distance the player can be from an interactable object and still be able to interact with it when playing in the normal game mode.")]
        public float MaxInteractDistance = 3f;
        [Tooltip("The max distance the player can be from an interactable object and still be able to interact with it when playing in VR mode.")]
        public float MaxInteractDistance_VR = 3f;
        
        
        [Header("General VR Parameters")]
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        public bool EnableVrMode = true;
        
        
        [Header("VR Controllers Parameters")]
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public bool VrLeftHand_EnableContinuousMovement = true;
        public bool VrLeftHand_EnableContinuousTurning;
        public bool VrLeftHand_EnableUiScrolling;
        public bool VrLeftHand_NearFarEnableTeleportDuringNearInteraction = true;
        
        [Space(10)]
        
        public bool VrRightHand_EnableContinuousMovement;
        public bool VrRightHand_EnableContinuousTurning;
        public bool VrRightHand_EnableUiScrolling = true;
        public bool VrRightHand_NearFarEnableTeleportDuringNearInteraction = true;
        

        [Header("Prefabs")]
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public Transform PlayerPrefab;
        public Transform PlayerPrefab_VR;
        public Transform UiSystemPrefab;
        public Transform UiSystemPrefab_VR;
        public Transform XR_Interaction_Manager_Prefab;
        public Transform XR_UI_EventSystem_Prefab;

    }
    
}
