using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponDistanceData", menuName = "ScriptableObjects/NewWeaponDistanceData", order = 1)]
public class DistanceWeaponData : WeaponData
{
    public ProjectileType projectileType;
    public int amountProjectilePerFire = 1;
    public float spread = 0f;
}