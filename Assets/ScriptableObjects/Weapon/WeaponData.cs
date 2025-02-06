using UnityEngine;

public abstract class WeaponData : ResourceObject
{
    public int damage = 1;
    public float attackRate = 1;
    public string audioClipName = "ShootDefault";
}