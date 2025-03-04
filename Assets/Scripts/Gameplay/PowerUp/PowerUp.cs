using UnityEngine;

public class PowerUp : ScriptableObject
{
    public string title;
    public string Description;
    public RarityEnum rarity;
    public virtual void OnUse()
    {
        Debug.Log(title);
    }
}
