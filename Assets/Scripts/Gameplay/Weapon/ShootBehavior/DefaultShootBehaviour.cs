using DG.Tweening;
using UnityEngine;

public class DefaultShootBehaviour : ShootBehaviour
{
    public override void SetupProjectile(Weapon weapon, Projectile projectile, int i)
    {
        base.SetupProjectile(weapon, projectile, i);

        Vector3 bulletPosition = weapon.firePoint.localPosition;

        float spacing = projectile.ProjectileData.size;

        var posX =
            (i * (projectile.ProjectileData.size + spacing))
            - ((weapon.WeaponData.amountBullet / 2f) * (projectile.ProjectileData.size))
            - ((projectile.ProjectileData.size + spacing) / 2)
            - (spacing * (weapon.WeaponData.amountBullet / 2f));


        bulletPosition.x += posX;

        projectile.transform.SetLocalPositionAndRotation(bulletPosition, Quaternion.identity);
    }
}
