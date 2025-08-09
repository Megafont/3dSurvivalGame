using UnityEngine;

namespace SurvivalGame.Player
{
    public class Player : MonoBehaviour, IPlayer
    {
        public Camera Camera { get; private set; }
        
        
        private void Awake()
        {
            Camera = GetComponentInChildren<Camera>();    
        }
    }

}