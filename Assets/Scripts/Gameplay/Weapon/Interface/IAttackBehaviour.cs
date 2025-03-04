using UnityEngine;

public interface IAttackBehaviour
{
    void Attack(Weapon weapon);

    public void Upgrade(params object[] args);
}

public interface IProjectileBehaviour
{
    /// <summary>
    /// Méthode appelée lors de l’impact du projectile.
    /// </summary>
    void OnHit();
}


