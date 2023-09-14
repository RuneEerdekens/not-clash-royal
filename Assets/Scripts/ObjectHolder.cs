using UnityEngine;

[CreateAssetMenu(fileName = "Obj", order = 1)]
public class ObjectHolder : ScriptableObject
{
    public GameObject ObjectHull;
    public GameObject Object;
    public float Cost;
}
