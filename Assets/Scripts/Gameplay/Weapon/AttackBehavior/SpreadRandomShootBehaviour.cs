using UnityEngine;

public class SpreadRandomShootBehaviour : DistanceAttackBehaviour
{
    public override void SetupProjectile(Weapon weapon, Projectile projectile, int i)
    {
        base.SetupProjectile(weapon, projectile, i);

        DistanceWeaponData data = (DistanceWeaponData)weapon.weaponData;

        float spreadAngle = data.spread;

        float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

        Quaternion spreadRotation = Quaternion.AngleAxis(randomAngle, Vector3.up);

        Quaternion finalRotation = weapon.firePoint.rotation * spreadRotation;

        projectile.transform.SetPositionAndRotation(weapon.firePoint.position, finalRotation);
    }
}
