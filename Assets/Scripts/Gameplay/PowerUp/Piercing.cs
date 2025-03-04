using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Piercing", order = 1)]
public class Piercing : PowerUp
{
    [SerializeField] private int pierceCount = 2;

    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<PiercingProjectileDecorator>(pierceCount);
    }
}