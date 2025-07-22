using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Jump;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;


namespace SurvivalGame.Utils
{
    public static class VrUtils
    {
        public static void InitVrSystem(ParametersManager gameParameters, Transform vrSystemParent)
        {
            // If a regular UI EventSystem is present, we need to remove it, as it can conflict with the XR UI EventSystem.
            EventSystem[] eventSystems = GameObject.FindObjectsByType<EventSystem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (eventSystems.Length > 0)
            {
                for (int i = 0; i < eventSystems.Length; i++)
                    GameObject.Destroy(eventSystems[i].gameObject);
            }
            
            
            Transform xrInteractionSystem = GameObject.Instantiate(gameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_PREFAB_XR_INTERACTION_MANAGER).Value,
                Vector3.zero, Quaternion.identity, vrSystemParent);
            
            Transform xrUiEventSystem = GameObject.Instantiate(gameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_PREFAB_XR_UI_EVENT_SYSTEM).Value,
                Vector3.zero, Quaternion.identity, vrSystemParent);
        }

        public static void InitVrPlayerObject(GameObject vrPlayerObject, ParametersManager gameParameters)
        {
            // Initialize the vr player object's movement parameters.
            DynamicMoveProvider dynamicMoveProvider = vrPlayerObject.GetComponentInChildren<DynamicMoveProvider>();
            dynamicMoveProvider.moveSpeed = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_MOVEMENT_SPEED_VR).Value;
            
            // Initialize the vr player object's look parameters.
            SnapTurnProvider snapTurnProvider = vrPlayerObject.GetComponentInChildren<SnapTurnProvider>();
            snapTurnProvider.turnAmount = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_MOVEMENT_SNAP_TURN_SPEED_VR).Value;
            
            ContinuousTurnProvider continuousTurnProvider = vrPlayerObject.GetComponentInChildren<ContinuousTurnProvider>();
            continuousTurnProvider.turnSpeed = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_MOVEMENT_SNAP_TURN_SPEED_VR).Value;
            
            
            // Initialize the vr player object's jump parameters.
            JumpProvider jumpProvider = vrPlayerObject.GetComponentInChildren<JumpProvider>();
            jumpProvider.jumpHeight = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_MOVEMENT_JUMP_HEIGHT_VR).Value;
            
            
            // Initialize the vr player object's controller objects settings.
            InitVrPlayerControllerSettings(vrPlayerObject, gameParameters);
        }

        public static void InitVrPlayerControllerSettings(GameObject vrPlayerObject, ParametersManager gameParameters)
        {
            // Initialize the vr player object's controller objects settings.
            ControllerInputActionManager[] controllerInputActionManagers = vrPlayerObject.GetComponentsInChildren<ControllerInputActionManager>(true);
            if (controllerInputActionManagers.Length > 2)
                Debug.LogError("More than 2 ControllerInputActionManagers were found in the scene! Is this intentional? There should be one on the \"Left Controller\" object and one on the \"Right Controller\" object.");
            else if (controllerInputActionManagers.Length < 1)
                Debug.LogError("No ControllerInputActionManagers were found in the scene! Is this intentional? There should be one on the \"Left Controller\" object and one on the \"Right Controller\" object.");

            
            bool left = false;
            bool right = false;
            foreach (ControllerInputActionManager controllerInputActionManager in controllerInputActionManagers)
            {
                if (controllerInputActionManager.transform.name == "Left Controller")
                {
                    // Initialize the left hand controller settings.
                    controllerInputActionManager.smoothMotionEnabled = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_ENABLE_CONTINUOUS_MOVEMENT).Value;
                    controllerInputActionManager.smoothTurnEnabled = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_ENABLE_CONTINUOUS_TURNING).Value;
                    controllerInputActionManager.uiScrollingEnabled = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_ENABLE_UI_SCROLLING).Value;
                    
                    // NOTE: The ControllerInputActionManager class is part of the StarterAssets package under XR Toolkit in the package manager.
                    //       I modified that script to add in a public property for NearFarEnableTeleportDuringNearInteraction so this line can work.
                    controllerInputActionManager.NearFarEnableTeleportDuringNearInteraction = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_NEAR_FAR_ENABLE_TELEPORT_DURING_NEAR_INTERACTION).Value;
                    
                    left = true;
                }
                else if (controllerInputActionManager.transform.name == "Right Controller")
                {
                    // Initialize the right hand controller settings.
                    controllerInputActionManager.smoothMotionEnabled = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_ENABLE_CONTINUOUS_MOVEMENT).Value;
                    controllerInputActionManager.smoothTurnEnabled = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_ENABLE_CONTINUOUS_TURNING).Value;
                    controllerInputActionManager.uiScrollingEnabled = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_ENABLE_UI_SCROLLING).Value;
                    
                    // NOTE: The ControllerInputActionManager class is part of the StarterAssets package under XR Toolkit in the package manager.
                    //       I modified that script to add in a public property for NearFarEnableTeleportDuringNearInteraction so this line can work.
                    controllerInputActionManager.NearFarEnableTeleportDuringNearInteraction = gameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_NEAR_FAR_ENABLE_TELEPORT_DURING_NEAR_INTERACTION).Value;
                    
                    right = true;
                }
                else
                {
                    Debug.LogWarning($"The ControllerInputActionManager on object \"{controllerInputActionManager.gameObject.name}\" is not on the \"Left Controller\" or \"Right Controller\" objects. Is this intentional?");    
                }
            } // end foreach
            
            
            if (!left)
                Debug.LogError($"The \"Left Controller\" object is missing a ControllerInputActionManager component, as none of the ones found are on that object. Is this intentional?");
            
            if (!right)
                Debug.LogError($"The \"Right Controller\" object is missing a ControllerInputActionManager component, as none of the ones found are on that object. Is this intentional?");            
        }
        
        
    }

}