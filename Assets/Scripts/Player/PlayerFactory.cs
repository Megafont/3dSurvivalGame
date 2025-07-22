using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Utils;
using UnityEngine;

namespace SurvivalGame.Player
{
    public static class PlayerFactory
    {
        public static GameObject CreatePlayer(Transform spawnPoint, ParametersManager gameParameters)
        {
            GameObject playerObject = GameObject.Instantiate(gameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_PREFAB).Value.gameObject, 
                                                             spawnPoint.position,
                                                             Quaternion.identity);

            InitPlayer(playerObject, gameParameters);
            
            return playerObject;
        }

        public static GameObject CreatePlayerVR(Transform spawnPoint, ParametersManager gameParameters)
        {
            GameObject playerObject = GameObject.Instantiate(gameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_PREFAB_VR).Value.gameObject, 
                                                             spawnPoint.position,
                                                             Quaternion.identity);

            InitPlayer(playerObject, gameParameters);
            VrUtils.InitVrPlayerObject(playerObject, gameParameters);
            
            return playerObject;
        }

        private static void InitPlayer(GameObject playerObject, ParametersManager gameParameters)
        {
            // Initialize the character controller's parameters
            CharacterController characterController = playerObject.GetComponent<CharacterController>();
            
            characterController.slopeLimit = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_CONTROLLER_SLOPE_LIMIT).Value;
            characterController.stepOffset = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_CONTROLLER_STEP_OFFSET).Value;
            characterController.skinWidth = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_CONTROLLER_SKIN_WIDTH).Value;
            characterController.minMoveDistance = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_CONTROLLER_MIN_MOVE_DISTANCE).Value;
            characterController.center = gameParameters.GetParameterData<Vector3>(ParameterIDs.PLAYER_CONTROLLER_CENTER).Value;
            characterController.radius = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_CONTROLLER_RADIUS).Value;
            characterController.height = gameParameters.GetParameterData<float>(ParameterIDs.PLAYER_CONTROLLER_HEIGHT).Value;
        }
    }
}