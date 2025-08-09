using System;
using SurvivalGame.Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace SurvivalGame.Objects.Interactables
{
    public class InteractableObject_Base : MonoBehaviour, IInteractableObject
    {

        [SerializeField] private ObjectData_Base _ObjectData;

        
        private void Awake()
        {
            if (_ObjectData == null)
                throw new Exception(
                    $"Interactable object \"{gameObject.name}\" does not have its ObjectData field set in the inspector!");


            if (_ObjectData.OverridePrefabScale)
                transform.localScale = _ObjectData.ScaleOverrideValue;
        }
        
        public string GetItemName()
        {
            return _ObjectData.Name;
        }

        public void Interact()
        {
            if (_ObjectData.CanBePickedUp)
                DoPickUpAction();
            else if (_ObjectData.IsInteractable)
                DoInteractAction();
        }

        protected virtual void DoInteractAction()
        {
            
        }
        
        protected virtual void DoPickUpAction()
        {
            Destroy(gameObject);
        }
    }
}
