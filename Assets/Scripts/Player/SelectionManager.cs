using SurvivalGame.Objects;
using SurvivalGame.Objects.Interactables;
using SurvivalGame.Player;
using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Systems;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    public SelectionManager Instance { get; private set; }

    
    public bool OnTarget { get; private set; }

    private IPlayer _Player;
    private PlayerInputs _PlayerInputs;

    private Transform _SelectionTransform;

    
    private TextMeshProUGUI _InteractionInfoText;

    private ParameterData<float> _MaxInteractDistance;


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
            Debug.LogWarning($"SelectionManager on game object \"{gameObject.name}\" failed to find PlayerInputs object! It will keep trying.");
            return;
        }        
    }
    
    private void Start()
    {
        _InteractionInfoText = GameManager.Instance.UiSystemParent.Find("InteractionInfoText").GetComponentInChildren<TextMeshProUGUI>();
        
        _MaxInteractDistance = GameManager.Instance.GameParameters.GetParameterData<float>(ParameterIDs.PLAYER_GAMEPLAY_MAX_INTERACT_DISTANCE);
    }

    void Update()
    {
        if (Camera.main == null || _PlayerInputs == null)
        {
            _PlayerInputs = PlayerInputs.Instance;
            return;
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, _MaxInteractDistance.Value))
        {
            var selectionTransform = hit.transform;

            IInteractableObject interactableObject = selectionTransform.GetComponent<IInteractableObject>();
            
            // If the InteractableObject component is not on this object, check its parent if it has one.
            if (interactableObject == null && selectionTransform.parent != null)
                interactableObject = selectionTransform.parent.GetComponent<IInteractableObject>();

            // If we found an InteractableObject component, show the info text
            if (interactableObject != null)
            {
                if (_PlayerInputs.Pickup)
                {
                    interactableObject.Interact();
                }
                else
                {
                    _InteractionInfoText.text = interactableObject.GetItemName();
                    
                    // NOTE: We don't have code here to position the text object like in the SelectionManager_VR script.
                    //       This is because in Non-VR mode, this text object is just on the HUD canvas.
                    _InteractionInfoText.gameObject.SetActive(true);

                    // Return here so we don't disable the text object below.
                    return;
                }
            }

        }

        
        _InteractionInfoText.gameObject.SetActive(false);

    }

}
