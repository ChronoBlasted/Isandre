using UnityEngine;
using static PlayerAnimation;

public class PunchAttackBehaviour : AttackBehaviour
{
    Weapon weapon;

    public override void Attack(Weapon _weapon)
    {
        weapon = _weapon;

        MeleeWeaponData data = (MeleeWeaponData)weapon.WeaponData;

        PlayerManager.Instance.playerAnimation.animator.SetTrigger(PLAYER_ANIMATION_PARAMETER.MELEE_RIGHT.ToString());

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, data.range, weapon.layerToAttack);

        foreach (Collider collider in hitEnemies)
        {
            Debug.Log("Hit attack layers");
        }

        base.Attack(weapon);
    }

    private void OnDrawGizmos()
    {
        MeleeWeaponData data = (MeleeWeaponData)weapon.WeaponData;

        Gizmos.DrawWireSphere(transform.position + transform.forward, data.range);
    }
}
