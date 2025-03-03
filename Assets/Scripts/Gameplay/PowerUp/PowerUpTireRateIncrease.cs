using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/TireRate", order = 1)]
public class PowerUpTireRateIncrease : PowerUp
{
    [SerializeField] float attackRateMultiplier;
    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.currentWeapon.weaponData.attackRate = PlayerManager.Instance.playerWeapon.currentWeapon.weaponData.attackRate * attackRateMultiplier;
    }
}

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Explosive", order = 1)]
public class Explosive : PowerUp
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionDamage = 5f;
    [SerializeField] private float explosionForce = 5f;
    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<ExplosiveProjectileDecorator>(explosionRadius, explosionDamage, explosionForce);
    }
}
