using UnityEngine;

[CreateAssetMenu(fileName = "NewResourceName", menuName = "ScriptableObjects/NewResourceObject", order = 0)]
public class ResourceObject : ScriptableObject
{
    public ResourceType type;
    public new string name;
    public Sprite sprite;
    public GameObject prefab;
}