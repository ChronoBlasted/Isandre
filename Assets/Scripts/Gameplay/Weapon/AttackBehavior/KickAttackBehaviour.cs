using UnityEngine;
using static PlayerAnimation;

public class KickAttackBehaviour : AttackBehaviour
{
    Weapon weapon;

    public override void Attack(Weapon _weapon)
    {
        weapon = _weapon;

        MeleeWeaponData data = (MeleeWeaponData)weapon.weaponData;

        PlayerManager.Instance.playerAnimation.animator.SetTrigger(PLAYER_ANIMATION_PARAMETER.KICK_RIGHT.ToString());

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, data.range, weapon.layerToAttack);

        foreach (Collider collider in hitEnemies)
        {
            if (collider.gameObject.TryGetComponent(out Alive _alive))
            {
                _alive.ChangeLife(-weapon.weaponData.damage);
            }
        }

        base.Attack(weapon);
    }

    private void OnDrawGizmos()
    {
        MeleeWeaponData data = (MeleeWeaponData)weapon.weaponData;

        Gizmos.DrawWireSphere(transform.position + transform.forward, data.range);
    }
}
