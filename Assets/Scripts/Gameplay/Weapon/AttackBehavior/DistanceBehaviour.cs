using UnityEngine;
using static PlayerAnimation;

public abstract class DistanceBehaviour : AttackBehaviour
{
    public override void Attack(Weapon weapon)
    {
        base.Attack(weapon);

        DistanceWeaponData data = (DistanceWeaponData)weapon.WeaponData;

        PlayerManager.Instance.playerAnimation.animator.SetTrigger(PLAYER_ANIMATION_PARAMETER.SHOOT_RIGHT.ToString());

        for (int i = 1; i <= data.amountProjectilePerFire; i++)
        {
            GameObject bullet = PoolManager.Instance[(ResourceType)data.projectileType].Get();
            bullet.layer = weapon.layerToAttack;

            Projectile projectile = bullet.GetComponent<Projectile>();

            bullet.transform.SetParent(weapon.firePoint);
            SetupProjectile(weapon, projectile, i);
            bullet.transform.SetParent(null);

            projectile.Init();
        }
    }


    public virtual void SetupProjectile(Weapon weapon, Projectile projectile, int i)
    {

    }

}
