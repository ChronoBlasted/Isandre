using DG.Tweening;
using UnityEngine;

public class DefaultShootBehaviour : DistanceBehaviour
{
    public override void SetupProjectile(Weapon weapon, Projectile projectile, int i)
    {
        base.SetupProjectile(weapon, projectile, i);

        DistanceWeaponData data = (DistanceWeaponData)weapon.WeaponData;

        Vector3 bulletPosition = weapon.firePoint.localPosition;

        float spacing = projectile.ProjectileData.size;

        var posX =
            (i * (projectile.ProjectileData.size + spacing))
            - ((data.amountProjectilePerFire / 2f) * (projectile.ProjectileData.size))
            - ((projectile.ProjectileData.size + spacing) / 2)
            - (spacing * (data.amountProjectilePerFire / 2f));


        bulletPosition.x += posX;

        projectile.transform.SetLocalPositionAndRotation(bulletPosition, Quaternion.identity);
    }
}
