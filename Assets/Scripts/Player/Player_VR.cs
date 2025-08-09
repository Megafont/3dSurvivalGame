using UnityEngine;

namespace SurvivalGame.Player
{
    public class Player_VR : MonoBehaviour, IPlayer
    {
        public Camera Camera { get; private set; }
        
        
        private void Awake()
        {
            Camera = GetComponentInChildren<Camera>();    
        }
    }
    
    
}