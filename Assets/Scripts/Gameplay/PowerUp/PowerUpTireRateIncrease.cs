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

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/MultiShoot", order = 1)]
public class MultiShoot : PowerUp
{
    [SerializeField] private int numberOfShots = 2;
    [SerializeField] private float angleSpread = 5f;
    
    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.AddDecorator<MultiShotDecorator>(numberOfShots, angleSpread);
    }
}

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Raffale", order = 1)]
public class Raffale : PowerUp
{
    [SerializeField] private int burstCount = 2;
    [SerializeField] private float timeBetweenShots = 0.5f;
    
    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.AddDecorator<BurstFireDecorator>(burstCount, timeBetweenShots);
    }
}

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Piercing", order = 1)]
public class Piercing : PowerUp
{
    [SerializeField] private int pierceCount = 2;
    
    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<PiercingProjectileDecorator>(pierceCount);
    }
}

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Chain", order = 1)]
public class Chain : PowerUp
{
    [SerializeField] private float chainRadius = 2;
    [SerializeField] private int chainNumber = 2;
    
    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<ChainProjectileDecorator>(chainRadius, chainNumber);
    }
}
