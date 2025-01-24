using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/NewWeaponData", order = 1)]
public class WeaponData : ResourceObject
{
    public ProjectileType projectileType;

    public float damage;
    public float firerate; // Balle par seconde
    public int amountBullet;
    public float spread = 0f;
}