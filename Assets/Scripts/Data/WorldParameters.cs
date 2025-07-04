using UnityEngine;

[CreateAssetMenu(fileName = "WorldParameters", menuName = "Scriptable Objects/WorldParameters")]
public class WorldParameters : ScriptableObject
{
    public Vector3 Gravity = new Vector3(0f, -9.81f, 0f);
}
