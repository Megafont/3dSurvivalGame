using UnityEngine;


namespace SurvivalGame.Utils
{
    public static class GameObjectUtils
    {
        public static void ClearAllChildren(Transform transform)
        {
            ClearAllChildren(transform.gameObject);
        }
        
        public static void ClearAllChildren(GameObject gameObject)
        {
            for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
    
}