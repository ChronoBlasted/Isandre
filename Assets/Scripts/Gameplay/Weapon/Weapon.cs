using UnityEngine;
using static PlayerAnimation;

public class Weapon : MonoBehaviour
{
    public WeaponData WeaponData;
    public Transform firePoint;
    public ShootBehaviour shootBehaviour;
    public LayerMask projectileLayerToApply;

    float timeSinceLastShot = 0f;

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void Fire()
    {
        if (timeSinceLastShot >= (1f / WeaponData.firerate))
        {
            PlayerManager.Instance.playerAnimation.animator.SetTrigger(PLAYER_ANIMATION_PARAMETER.SHOOT_RIGHT.ToString());

            shootBehaviour.Shoot(this);

            timeSinceLastShot = 0f;
        }
    }
}
