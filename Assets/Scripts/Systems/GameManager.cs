using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    

    [Header("Game Settings")] 
    
    [SerializeField]
    private GameParameters _GameParameters;
    
    [SerializeField]
    private Transform _PlayerSpawnPoint;

    

    private Transform _SystemsParent;
    
    private Transform _PlayerObject;
    
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("An instance of GameManager already exists in this scene! This one will now destroy itself to avoid problems.");
            
            Destroy(gameObject);
            return;
        }
        
        
        Instance = this;
        
        _SystemsParent = GameObject.Find("Systems").transform;
        
        InitGame();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitGame()
    {
        Physics.gravity = _GameParameters.WorldParameters.Gravity;
        
        
        if (!_GameParameters.EnableVrMode)
            InitPlayer();
        else
            InitPlayerVR();
    }

    private void InitPlayer()
    {
        //GameObject eventSystem = new GameObject("UI EventSystem");
        //eventSystem.transform.parent = _SystemsParent;
        //eventSystem.AddComponent<EventSystem>();
        
        _PlayerObject = Instantiate(_GameParameters.PlayerParameters.PlayerPrefab, _PlayerSpawnPoint.position, Quaternion.identity);
    }

    private void InitPlayerVR()
    {
        Transform uiEventSystem = Instantiate(_GameParameters.PlayerParameters.XR_Interaction_Manager_Prefab, Vector3.zero, Quaternion.identity, _SystemsParent);
        
        _PlayerObject = Instantiate(_GameParameters.PlayerParameters.PlayerPrefab_VR, _PlayerSpawnPoint.position, Quaternion.identity);
    }
    
    
    
    public GameParameters GameParameters { get { return _GameParameters; } }
}
