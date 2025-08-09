using System;
using System.Collections;
using UnityEngine;

using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Player;
using SurvivalGame.Utils;

using GameObjectUtils = SurvivalGame.Utils.GameObjectUtils;


namespace SurvivalGame.Systems
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event EventHandler VrModeChanged;
        
        public ParametersManager GameParameters { get; private set; }

        public Transform SystemsParent { get; private set; }
        public Transform UiSystemParent { get; private set; }
        public Transform VrSystemParent { get; private set; }
        
        public IPlayer PlayerObject { get; private set; }
        
        
        
        [SerializeField] private Transform _PlayerSpawnPoint;

        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning(
                    "An instance of GameManager already exists in this scene! This one will now destroy itself to avoid problems.");

                Destroy(gameObject);
                return;
            }


            Instance = this;

            
            GameParameters = GetComponent<ParametersManager>();
            
            SystemsParent = GameObject.Find("Systems").transform;
            
            VrSystemParent = SystemsParent.Find("VR");


            
            StartCoroutine(WaitForDependencies(new WaitForSeconds(0.5f)));
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator WaitForDependencies(WaitForSeconds interval)
        {
            // Wait until we find the ParametersManager, and it is initialized.
            while (true)
            {
                GameParameters = GetComponent<ParametersManager>();
                yield return interval; // Wait one frame.

                if (GameParameters != null && GameParameters.IsInitialized)
                    break;
            }

            
            InitGame();
        }
        
        private void InitGame()
        {
            Physics.gravity = GameParameters.GetParameterData<Vector3>(ParameterIDs.WORLD_GRAVITY).Value;


            if (GameParameters.GetParameterData<bool>(ParameterIDs.PLAYER_VR_ENABLE_VR_MODE).Value)
            {
                // First spawn the VR UI system.
                if (UiSystemParent != null)
                    DestroyImmediate(UiSystemParent.gameObject);
                UiSystemParent = Instantiate(GameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_UI_PREFAB_VR).Value, SystemsParent);

                // First let's initialize the VR system.
                if (VrSystemParent.childCount > 0)
                    GameObjectUtils.ClearAllChildren(VrSystemParent);
                    
                // Initialize the VR system.
                VrUtils.InitVrSystem(GameParameters, VrSystemParent);
                
                // Now spawn a VR player.
                if (PlayerObject != null)
                    DestroyImmediate(PlayerObject.gameObject);
                PlayerObject = PlayerFactory.CreatePlayerVR(_PlayerSpawnPoint, GameParameters);
            }
            else
            {
                // First spawn the UI system.
                if (UiSystemParent != null)
                    DestroyImmediate(UiSystemParent.gameObject);
                UiSystemParent = Instantiate(GameParameters.GetParameterData<Transform>(ParameterIDs.PLAYER_UI_PREFAB).Value, SystemsParent);
                
                // Next, let's remove any VR system components that may be present.
                if (VrSystemParent.childCount > 0)
                    GameObjectUtils.ClearAllChildren(VrSystemParent);
                
                // Now spawn a non-VR player.
                if (PlayerObject != null)
                    DestroyImmediate(PlayerObject.gameObject);
                PlayerObject = PlayerFactory.CreatePlayer(_PlayerSpawnPoint, GameParameters);
                
            }
            
            VrModeChanged?.Invoke(this, EventArgs.Empty);
        }
        
        
    }
    
}