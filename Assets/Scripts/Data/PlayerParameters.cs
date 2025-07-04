using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParams", menuName = "Scriptable Objects/PlayerParams")]
public class PlayerParameters : ScriptableObject
{
    [Header("Movement")]
    
    public float MovementSpeed = 12f;
    public float JumpHeight = 2f;
    
    
    [Header("Prefabs")]
    
    public Transform PlayerPrefab;
    public Transform PlayerPrefab_VR;
    public Transform XR_Interaction_Manager_Prefab;
  
}
