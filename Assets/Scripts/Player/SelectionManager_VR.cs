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
    public ControllerTypes ControllerType { get; private set; } = ControllerTypes.Unknown;

    
    public enum ControllerTypes
    {
        Unknown = 0,
        Left = 1,
        Right = 2,
    }


    private NearFarInteractor _NearFarInteractor;
    private TextMeshProUGUI _InteractionInfoText;

    private ParameterData<float> _MaxInteractDistance_VR;

    
    
    private void Awake()
    {
      
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
        }


        Debug.Log($"Parent: {transform.parent.name}    ControllerType: {ControllerType}    {_InteractionInfoText == null}");
        _MaxInteractDistance_VR = GameManager.Instance.GameParameters.GetParameterData<float>(ParameterIDs.PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE_VR);
    }

    void Update()
    {
        if (_NearFarInteractor == null)
            return;
        
        
        Ray ray = new Ray(_NearFarInteractor.transform.position, _NearFarInteractor.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _MaxInteractDistance_VR.Value))
        {
            var selectionTransform = hit.transform;

            string selectionName = "NULL";
            string parentName = "NULL";
            if (selectionTransform != null)
            {
                selectionName = selectionTransform.name;
                
                if (selectionTransform.parent != null)
                    parentName = selectionTransform.parent.name;
            }
                
            Debug.Log($"Selection: {selectionName}    Parent: {parentName}    Text: {(_InteractionInfoText != null ? _InteractionInfoText.name : "NULL")}");
            InteractableObject interactableObject = selectionTransform.GetComponent<InteractableObject>();
            
            // If the InteractableObject component is not on this object, check its parent.
            if (interactableObject == null && selectionTransform.parent != null)
                interactableObject = selectionTransform.parent.GetComponent<InteractableObject>();

            // If we found an InteractableObject component, show the info text
            if (interactableObject != null)
            {
                _InteractionInfoText.text = interactableObject.GetItemName();
                
                // Calculate the position of the text.
                Vector3 textPosOffset = hit.point;
                textPosOffset -= ray.direction.normalized;
                _InteractionInfoText.transform.parent.position = textPosOffset;
                
                // Make the canvas for this text face the player.
                // NOTE: The child text object is rotated 180 degrees to stop the text being backwards.
                _InteractionInfoText.transform.parent.LookAt(GameManager.Instance.PlayerObject.transform.position);
                
                _InteractionInfoText.gameObject.SetActive(true);

                return;
            }

        }

        
        _InteractionInfoText.gameObject.SetActive(false);

    }

}
