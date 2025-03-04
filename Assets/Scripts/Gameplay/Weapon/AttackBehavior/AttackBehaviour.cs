using UnityEngine;

public abstract class AttackBehaviour : IAttackBehaviour
{
    public virtual void Attack(Weapon weapon)
    {

    }

    public void Upgrade(params object[] args)
    {
        
    }
}
