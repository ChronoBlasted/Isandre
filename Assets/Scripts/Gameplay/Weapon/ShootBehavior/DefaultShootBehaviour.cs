using DG.Tweening;
using UnityEngine;

public class DefaultShootBehaviour : ShootBehaviour
{
    public override void SetupProjectile(Weapon weapon, Projectile projectile, int i)
    {
        base.SetupProjectile(weapon, projectile, i);

        Vector3 bulletPosition = weapon.firePoint.position;

        float spacing = .1f;
        bulletPosition.x += (i * spacing) - (spacing * (weapon.WeaponData.amountBullet / 2f));

        projectile.transform.SetPositionAndRotation(weapon.firePoint.position, weapon.firePoint.rotation);
    }
}
