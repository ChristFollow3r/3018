using UnityEngine;

[CreateAssetMenu(fileName = "CelestialBody", menuName = "Scriptable Objects/CelestialBody")]
public class CelestialBody : ScriptableObject
{
    public string bodyName;
    public float gravityForce;
    public float gravityRadius;
}
