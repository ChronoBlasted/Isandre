using UnityEngine;
using static PlayerAnimation;

public abstract class DistanceAttackBehaviour : AttackBehaviour
{
    public override void Attack(Weapon weapon)
    {
        base.Attack(weapon);

        DistanceWeaponData data = (DistanceWeaponData)weapon.weaponData;

        PlayerManager.Instance.playerAnimation.animator.SetTrigger(PLAYER_ANIMATION_PARAMETER.SHOOT_RIGHT.ToString());

        for (int i = 1; i <= data.amountProjectilePerFire; i++)
        {
            GameObject bullet = PoolManager.Instance[(ResourceType)data.projectileType].Get();
            bullet.layer = 16;

            Projectile projectile = bullet.GetComponent<Projectile>();

            bullet.transform.SetParent(weapon.firePoint);
            SetupProjectile(weapon, projectile, i);
            bullet.transform.SetParent(null);

            projectile.Init(weapon.weaponData.damage);
        }
    }


    public virtual void SetupProjectile(Weapon weapon, Projectile projectile, int i)
    {

    }

}
