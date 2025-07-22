namespace SurvivalGame.Systems.Data
{
    public enum ParameterIDs
    {
        // WORLD PARAMETERS IDS
        // ----------------------------------------------------------------------------------------------------

        WORLD_GRAVITY = 0,
        
        
        // PLAYER PARAMETER IDS
        // ----------------------------------------------------------------------------------------------------
        
        // Player controller parameters
        PLAYER_CONTROLLER_SLOPE_LIMIT = 10000,
        PLAYER_CONTROLLER_STEP_OFFSET,
        PLAYER_CONTROLLER_SKIN_WIDTH,
        PLAYER_CONTROLLER_MIN_MOVE_DISTANCE,
        PLAYER_CONTROLLER_CENTER,
        PLAYER_CONTROLLER_RADIUS,
        PLAYER_CONTROLLER_HEIGHT,
        
        // Player movement parameters
        PLAYER_MOVEMENT_SPEED,
        PLAYER_MOVEMENT_TURN_SPEED,
        PLAYER_MOVEMENT_JUMP_HEIGHT,
        
        // VR player movement parameters
        PLAYER_MOVEMENT_SPEED_VR,
        PLAYER_MOVEMENT_TURN_SPEED_VR,
        PLAYER_MOVEMENT_JUMP_HEIGHT_VR,
        PLAYER_MOVEMENT_SNAP_TURN_SPEED_VR,
        
        // Player Gameplay Parameters
        PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE,
        PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE_VR,
        
        // Player VR Specific Parameters
        PLAYER_VR_ENABLE_VR_MODE,
        
        // Player VR Left Hand Parameters
        PLAYER_VR_LEFTHAND_ENABLE_CONTINUOUS_MOVEMENT,
        PLAYER_VR_LEFTHAND_ENABLE_CONTINUOUS_TURNING,
        PLAYER_VR_LEFTHAND_ENABLE_UI_SCROLLING,
        PLAYER_VR_LEFTHAND_NEAR_FAR_ENABLE_TELEPORT_DURING_NEAR_INTERACTION,
        
        // Player VR Right Hand Parameters
        PLAYER_VR_RIGHTHAND_ENABLE_CONTINUOUS_MOVEMENT,
        PLAYER_VR_RIGHTHAND_ENABLE_CONTINUOUS_TURNING,
        PLAYER_VR_RIGHTHAND_ENABLE_UI_SCROLLING,
        PLAYER_VR_RIGHTHAND_NEAR_FAR_ENABLE_TELEPORT_DURING_NEAR_INTERACTION,
        
        // Player Prefab Parameters
        PLAYER_PREFAB,
        PLAYER_PREFAB_VR,
        PLAYER_UI_PREFAB,
        PLAYER_UI_PREFAB_VR,
        PLAYER_PREFAB_XR_INTERACTION_MANAGER,
        PLAYER_PREFAB_XR_UI_EVENT_SYSTEM,
    }
}