using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Scriptable Objects/GameParameters")]
public class GameParameters : ScriptableObject
{
    public WorldParameters WorldParameters;
    public PlayerParameters PlayerParameters;

    public bool EnableVrMode = false;
}
