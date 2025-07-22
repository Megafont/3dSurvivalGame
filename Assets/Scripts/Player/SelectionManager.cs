using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Systems;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    private TextMeshProUGUI _InteractionInfoText;

    private ParameterData<float> _MaxInteractDistance;
    
    
    private void Start()
    {
        _InteractionInfoText = GameManager.Instance.UiSystemParent.Find("InteractionInfoText").GetComponentInChildren<TextMeshProUGUI>();
        
        _MaxInteractDistance = GameManager.Instance.GameParameters.GetParameterData<float>(ParameterIDs.PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE);
    }

    void Update()
    {
        if (Camera.main == null)
            return;
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, _MaxInteractDistance.Value))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactableObject = selectionTransform.GetComponent<InteractableObject>();
            // If the InteractableObject component is not on this object, check its parent.
            if (interactableObject == null)
                interactableObject = selectionTransform.parent.GetComponent<InteractableObject>();

            // If we found an InteractableObject component, show the info text
            if (interactableObject != null)
            {
                _InteractionInfoText.text = interactableObject.GetItemName();
                _InteractionInfoText.gameObject.SetActive(true);

                return;
            }

        }

        
        _InteractionInfoText.gameObject.SetActive(false);

    }

}
