using System;
using System.Collections.Generic;

using UnityEngine;


namespace SurvivalGame.Systems.Data.ParamsManager
{
    public class ParametersManager : MonoBehaviour
    {
        [Header("General Parameters")]
        [SerializeField] private ParamDataSources _ParamDataSource = ParamDataSources.ScriptableObjects;
        
        
        [Header("Scriptable Object Parameter Data Sources")]
        [SerializeField] private WorldParameters  _WorldParameters;
        [SerializeField] private PlayerParameters _PlayerParameters;

        

        private Dictionary<Type, IParametersDictionary> _AllParameters = new Dictionary<Type, IParametersDictionary>();

        
        public bool IsInitialized { get; private set; }
        

        private void Awake()
        {
            LoadValues();
        }

        private void LoadValues()
        {
            switch (_ParamDataSource)
            {
                case ParamDataSources.ScriptableObjects:
                    LoadGameParamsFromScriptableObjects();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Could not load game parameters from unsupported data source: {Enum.GetName(typeof(ParamDataSources), _ParamDataSource)}");
            }    
        }
        
        private void LoadGameParamsFromScriptableObjects()
        {
            LoadWorldParamsFromScriptableObject();
            LoadPlayerParamsFromScriptableObject();

            IsInitialized = true;
        }
        
        
        private void LoadWorldParamsFromScriptableObject()
        { 
            AddParameter<Vector3>(ParameterIDs.WORLD_GRAVITY, _WorldParameters.Gravity);
        }
        
        
        private void LoadPlayerParamsFromScriptableObject()
        { 
            // Load in the player controller settings
            AddParameter<float>(ParameterIDs.PLAYER_CONTROLLER_SLOPE_LIMIT, _PlayerParameters.SlopeLimit);
            AddParameter<float>(ParameterIDs.PLAYER_CONTROLLER_STEP_OFFSET, _PlayerParameters.StepOffset);
            AddParameter<float>(ParameterIDs.PLAYER_CONTROLLER_SKIN_WIDTH, _PlayerParameters.SkinWidth);
            AddParameter<float>(ParameterIDs.PLAYER_CONTROLLER_MIN_MOVE_DISTANCE, _PlayerParameters.MinMoveDistance);
            AddParameter<Vector3>(ParameterIDs.PLAYER_CONTROLLER_CENTER, _PlayerParameters.Center);
            AddParameter<float>(ParameterIDs.PLAYER_CONTROLLER_RADIUS, _PlayerParameters.Radius);
            AddParameter<float>(ParameterIDs.PLAYER_CONTROLLER_HEIGHT, _PlayerParameters.Height);

            // Load in the player movement settings
            AddParameter<float>(ParameterIDs.PLAYER_MOVEMENT_SPEED, _PlayerParameters.MovementSpeed);
            AddParameter<float>(ParameterIDs.PLAYER_MOVEMENT_TURN_SPEED, _PlayerParameters.TurnSpeed);
            AddParameter<float>(ParameterIDs.PLAYER_MOVEMENT_JUMP_HEIGHT, _PlayerParameters.JumpHeight);
            
            // Load in the VR player movement settings
            AddParameter<float>(ParameterIDs.PLAYER_MOVEMENT_SPEED_VR, _PlayerParameters.MovementSpeed_VR);
            AddParameter<float>(ParameterIDs.PLAYER_MOVEMENT_TURN_SPEED_VR, _PlayerParameters.TurnSpeed_VR);
            AddParameter<float>(ParameterIDs.PLAYER_MOVEMENT_SNAP_TURN_SPEED_VR, _PlayerParameters.SnapTurnAmount_VR);
            AddParameter<float>(ParameterIDs.PLAYER_MOVEMENT_JUMP_HEIGHT_VR, _PlayerParameters.JumpHeight_VR);
            
            // Load in the player gameplay settings
            AddParameter<float>(ParameterIDs.PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE, _PlayerParameters.MaxInteractDistance);
            AddParameter<float>(ParameterIDs.PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE_VR, _PlayerParameters.MaxInteractDistance_VR);
            
            // Load in the player VR settings
            AddParameter<bool>(ParameterIDs.PLAYER_VR_ENABLE_VR_MODE, _PlayerParameters.EnableVrMode);
            
            // Load in the player VR left hand settings
            AddParameter<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_ENABLE_CONTINUOUS_MOVEMENT, _PlayerParameters.VrLeftHand_EnableContinuousMovement);
            AddParameter<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_ENABLE_CONTINUOUS_TURNING, _PlayerParameters.VrLeftHand_EnableContinuousTurning);
            AddParameter<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_ENABLE_UI_SCROLLING, _PlayerParameters.VrLeftHand_EnableUiScrolling);
            AddParameter<bool>(ParameterIDs.PLAYER_VR_LEFTHAND_NEAR_FAR_ENABLE_TELEPORT_DURING_NEAR_INTERACTION, _PlayerParameters.VrLeftHand_NearFarEnableTeleportDuringNearInteraction);
            
            // Load in the player VR right hand settings
            AddParameter<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_ENABLE_CONTINUOUS_MOVEMENT, _PlayerParameters.VrRightHand_EnableContinuousMovement);
            AddParameter<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_ENABLE_CONTINUOUS_TURNING, _PlayerParameters.VrRightHand_EnableContinuousTurning);
            AddParameter<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_ENABLE_UI_SCROLLING, _PlayerParameters.VrRightHand_EnableUiScrolling);
            AddParameter<bool>(ParameterIDs.PLAYER_VR_RIGHTHAND_NEAR_FAR_ENABLE_TELEPORT_DURING_NEAR_INTERACTION, _PlayerParameters.VrRightHand_NearFarEnableTeleportDuringNearInteraction);

            // Load in the player prefab settings
            AddParameter<Transform>(ParameterIDs.PLAYER_PREFAB, _PlayerParameters.PlayerPrefab);
            AddParameter<Transform>(ParameterIDs.PLAYER_PREFAB_VR, _PlayerParameters.PlayerPrefab_VR);
            AddParameter<Transform>(ParameterIDs.PLAYER_UI_PREFAB, _PlayerParameters.UiSystemPrefab);
            AddParameter<Transform>(ParameterIDs.PLAYER_UI_PREFAB_VR, _PlayerParameters.UiSystemPrefab_VR);
            AddParameter<Transform>(ParameterIDs.PLAYER_PREFAB_XR_INTERACTION_MANAGER, _PlayerParameters.XR_Interaction_Manager_Prefab);
            AddParameter<Transform>(ParameterIDs.PLAYER_PREFAB_XR_UI_EVENT_SYSTEM, _PlayerParameters.XR_UI_EventSystem_Prefab);
        }

        public void AddParameter<T>(ParameterIDs id, T value)
        {
            bool result = _AllParameters.TryGetValue(typeof(T), out IParametersDictionary dict);

            if (result)
            {
                ParametersDictionary<T> typedDict = dict as ParametersDictionary<T>;
                if (typedDict == null)
                    throw new Exception("The returned dictionary is not of the expected type!");
                
                typedDict.Add(id, value);
            }
            else
            {
                ParametersDictionary<T> newDict = new ParametersDictionary<T>();
                newDict.Add(id, value);
                _AllParameters.Add(typeof(T), newDict);
            }
            
        }
        
                
        public ParameterData<T> GetParameterData<T>(ParameterIDs id)
        {
            bool result = _AllParameters.TryGetValue(typeof(T), out var dict);
            if (result)
            {
                // Find the dictionary for type T
                ParametersDictionary<T> typedDict = dict as ParametersDictionary<T>;
                if (typedDict == null)
                {
                    Debug.LogError("The returned dictionary is not of the expected type!");
                    return null;
                }

                // Search that dictionary for the specified parameter ID.
                bool result2 = typedDict.TryGetValue(id, out ParameterData<T> data);
                if (result2)
                    return data;
                
                Debug.LogError($"Parameter {id} was not found!");
            }


            Debug.LogError($"The dictionary for parameters of type \"{typeof(T).Name}\" was not found!");
            return null;
        }        
    }
    
}