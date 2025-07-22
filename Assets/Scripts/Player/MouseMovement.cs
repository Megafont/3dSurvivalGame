using UnityEngine;

using SurvivalGame.Systems.Data;
using SurvivalGame.Systems.Data.ParamsManager;
using SurvivalGame.Systems;


public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
 
    float xRotation = 0f;
    float YRotation = 0f;

    private ParameterData<float> _TurnSpeed;

    private void Awake()
    {
        
    }
    
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
        
        _TurnSpeed = GameManager.Instance.GameParameters.GetParameterData<float>(ParameterIDs.PLAYER_MOVEMENT_TURN_SPEED);
    }
 
    void Update()
    {
        if (_TurnSpeed == null)
            return;
        
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
        // Control rotation around X-axis (Look up and down)
        xRotation -= Mathf.Clamp(mouseY, -_TurnSpeed.Value, _TurnSpeed.Value);
 
        // Clamp the rotation so we cant over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
        // Control rotation around Y-axis (Look up and down)
        YRotation += Mathf.Clamp(mouseX, -_TurnSpeed.Value, _TurnSpeed.Value);
 
        // Apply both rotations
        transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
 
    }
    
}
