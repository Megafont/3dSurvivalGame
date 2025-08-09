using UnityEngine;

namespace SurvivalGame.Objects
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "Scriptable Objects/Objects/Object Data")]

    public class ObjectData_Base : ScriptableObject
    {
        [Header("General Object Data")]
        public string Name;
        public string Description;

        public bool OverridePrefabScale;
        public Vector3 ScaleOverrideValue = Vector3.one;        
        
        
        [Header("Interaction Options")]
        public bool IsInteractable;
        public bool CanBePickedUp;
    }
}