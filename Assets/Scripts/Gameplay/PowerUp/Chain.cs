using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Chain", order = 1)]
public class Chain : PowerUp
{
    [SerializeField] private float chainRadius = 2;
    [SerializeField] private int chainNumber = 2;

    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<ChainProjectileDecorator>(chainRadius, chainNumber);
    }
}