using UnityEngine;

namespace SurvivalGame.Player
{
    public interface IPlayer
    {
        public Camera Camera { get; }
        public GameObject gameObject { get; }
        public Transform transform { get; }
    }
}