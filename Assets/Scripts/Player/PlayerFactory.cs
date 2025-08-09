using System;
using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Utils;
using UnityEngine;

namespace SurvivalGame.Player
{
    public static class PlayerFactory
    {
/* <<<<<<<<<<<<<<  ✨ Windsurf Command ⭐ >>>>>>>>>>>>>>>> */
        /// <summary>
        /// Creates a new player object.
        /// </summary>
        /// <param name="spawnPoint">The spawn point of the player.</param>
        /// <param name="gameParameters">The game parameters.</param>
        /// <returns>The created player object.</returns>
/* <<<<<<<<<<  8c75ab3e-1415-4953-8be3-8ee89e819d3c  >>>>>>>>>>> */
        public static IPlayer CreatePlayer(Transform spawnPoint, ParametersManager gameParameters)
        {
            GameObject playerObject = GameObject.Instantiate(gameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_PREFAB).Value.gameObject, 
                                                             spawnPoint.position,
                                                             Quaternion.identity);

            InitPlayer(playerObject, gameParameters);
            
            IPlayer player = playerObject.GetComponent<IPlayer>();
            if (player == null)
                throw new Exception("The player prefab does not contain a Player component!");

            return player;
        }

        public static IPlayer CreatePlayerVR(Transform spawnPoint, ParametersManager gameParameters)
        {
            GameObject playerObject = GameObject.Instantiate(gameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_PREFAB_VR).Value.gameObject, 
                                                             spawnPoint.position,
                                                             Quaternion.identity);

            InitPlayer(playerObject, gameParameters);
            VrUtils.InitVrPlayerObject(playerObject, gameParameters);
            
            IPlayer player = playerObject.GetComponent<IPlayer>();
            if (player == null)
                throw new Exception("The VR player prefab does not contain a Player_VR component!");

            return player;        
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