using SurvivalGame.Objects;
using SurvivalGame.Objects.Interactables;
using SurvivalGame.Player;
using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Systems;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SelectionManager_VR : MonoBehaviour
{
    public const float TEXT_DISTANCE_FROM_TARGET_OBJECT = 0.5f;
    public SelectionManager_VR Instance { get; private set; }
    
    
    public ControllerTypes ControllerType { get; private set; } = ControllerTypes.Unknown;
    public bool OnTarget { get; private set; }

    
    public enum ControllerTypes
    {
        Unknown = 0,
        Left = 1,
        Right = 2,
    }


    private IPlayer _Player;
    private PlayerInputs _PlayerInputs;

    private Transform _SelectionTransform;

    private NearFarInteractor _NearFarInteractor;
    private TextMeshProUGUI _InteractionInfoText;

    private ParameterData<float> _MaxInteractDistance_VR;

    
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        _Player = GameManager.Instance.PlayerObject;
        _PlayerInputs = PlayerInputs.Instance;
        if (_PlayerInputs == null)
        {
            Debug.LogWarning($"SelectionManager_VR on game object \"{gameObject.name}\" failed to find PlayerInputs object! It will keep trying.");
            return;
        }
    }
    
    private void Start()
    {
        if (transform.name == "Left Controller")
        {
            ControllerType = ControllerTypes.Left;
            _InteractionInfoText = GameManager.Instance.UiSystemParent.Find("InteractionInfoText_Left").GetComponentInChildren<TextMeshProUGUI>();
            _InteractionInfoText.gameObject.SetActive(false);
            _NearFarInteractor = GetComponentInChildren<NearFarInteractor>();
        }
        else if (transform.name == "Right Controller")
        {
            ControllerType = ControllerTypes.Right;
            _InteractionInfoText = GameManager.Instance.UiSystemParent.Find("InteractionInfoText_Right").GetComponentInChildren<TextMeshProUGUI>();
            _InteractionInfoText.gameObject.SetActive(false);
            _NearFarInteractor = GetComponentInChildren<NearFarInteractor>();
        }
        else
        {
            ControllerType = ControllerTypes.Unknown;
            _InteractionInfoText = null;
            _NearFarInteractor = null;
            
            Debug.LogError($"SelectionManager_VR on game object \"{gameObject.name}\" failed to determine which hand it belongs to!");
        }


        Debug.Log($"Parent: {transform.parent.name}    ControllerType: {ControllerType}    {_InteractionInfoText == null}");
        _MaxInteractDistance_VR = GameManager.Instance.GameParameters.GetParameterData<float>(ParameterIDs.PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE_VR);
    }

    void Update()
    {
        if (_NearFarInteractor == null || _PlayerInputs == null)
        {
            _PlayerInputs = PlayerInputs.Instance;
            return;
        }


        Ray ray = new Ray(_NearFarInteractor.transform.position, _NearFarInteractor.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _MaxInteractDistance_VR.Value))
        {
            _SelectionTransform = hit.transform;

            string selectionName = "NULL";
            string parentName = "NULL";
            if (_SelectionTransform != null)
            {
                selectionName = _SelectionTransform.name;
                
                if (_SelectionTransform.parent != null)
                    parentName = _SelectionTransform.parent.name;
            }
                
            //Debug.Log($"Selection: {selectionName}    Parent: {parentName}    Text: {(_InteractionInfoText != null ? _InteractionInfoText.name : "NULL")}");
            IInteractableObject interactableObject = _SelectionTransform.GetComponent<IInteractableObject>();
            
            // If the InteractableObject component is not on this object, check its parent if it has one.
            if (interactableObject == null && _SelectionTransform.parent != null)
                interactableObject = _SelectionTransform.parent.GetComponent<IInteractableObject>();

            // If we found an InteractableObject component, show the info text
            if (interactableObject != null)
            {
                if ((ControllerType == ControllerTypes.Left && _PlayerInputs.Pickup_VR_LeftHand) ||
                    (ControllerType == ControllerTypes.Right && _PlayerInputs.Pickup_VR_RightHand))
                {
                    interactableObject.Interact();
                }
                else
                {
                    
                    _InteractionInfoText.text = interactableObject.GetItemName();

                    // Calculate the position of the text.
                    Vector3 textPosOffset = hit.point;
                    textPosOffset -= ray.direction.normalized * TEXT_DISTANCE_FROM_TARGET_OBJECT;
                    _InteractionInfoText.transform.parent.position = textPosOffset;

                    // NOTE: The child text object is rotated 180 degrees to stop the text being backwards.
                    _InteractionInfoText.transform.parent.LookAt(_Player.Camera.transform.position);

                    _InteractionInfoText.gameObject.SetActive(true);

                    // Return here so we don't disable the text object below.
                    return;
                }

            }

        }

        
        _InteractionInfoText.gameObject.SetActive(false);

    }

}
