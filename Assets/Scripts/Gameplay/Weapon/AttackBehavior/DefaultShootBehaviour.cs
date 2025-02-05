using DG.Tweening;
using UnityEngine;

public class DefaultShootBehaviour : DistanceAttackBehaviour
{
    public override void SetupProjectile(Weapon weapon, Projectile projectile, int i)
    {
        base.SetupProjectile(weapon, projectile, i);

        DistanceWeaponData data = (DistanceWeaponData)weapon.weaponData;

        Vector3 bulletPosition = weapon.firePoint.localPosition;

        float spacing = projectile.projectileData.size;

        var posX =
            (i * (projectile.projectileData.size + spacing))
            - ((data.amountProjectilePerFire / 2f) * (projectile.projectileData.size))
            - ((projectile.projectileData.size + spacing) / 2)
            - (spacing * (data.amountProjectilePerFire / 2f));


        bulletPosition.x += posX;

        projectile.transform.SetLocalPositionAndRotation(bulletPosition, Quaternion.identity);
    }
}
