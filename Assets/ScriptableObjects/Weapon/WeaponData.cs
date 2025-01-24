using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/NewWeaponData", order = 1)]
public class WeaponData : ResourceObject
{
    public ProjectileType projectileType;

    public float damage = 1;
    public float attackRate = 1;
    public int amountPerAttack = 1;
    public float attackAngle = 0f;
    public float attackRange = 100f;
}