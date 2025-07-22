using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private string _ItemName;
 
    
    public string GetItemName()
    {
        return _ItemName;
    }
}
