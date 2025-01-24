using UnityEngine;
using static PlayerAnimation;

public class CloseAttackBehaviour : AttackBehaviour
{
    public override void Attack(Weapon weapon)
    {
        PlayerManager.Instance.playerAnimation.animator.SetTrigger(PLAYER_ANIMATION_PARAMETER.KICK.ToString());

        base.Attack(weapon);
    }
}
