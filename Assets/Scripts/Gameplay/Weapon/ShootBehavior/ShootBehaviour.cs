using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public virtual void Shoot(Weapon weapon)
    {
        for (int i = 1; i <= weapon.WeaponData.amountBullet; i++)
        {
            GameObject bullet = PoolManager.Instance[(ResourceType)weapon.WeaponData.projectileType].Get();
            bullet.layer = (int)Mathf.Log(weapon.projectileLayerToApply.value, 2);

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
