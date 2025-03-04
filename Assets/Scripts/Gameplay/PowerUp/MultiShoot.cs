using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/MultiShoot", order = 1)]
public class MultiShoot : PowerUp
{
    [SerializeField] private int numberOfShots = 2;
    [SerializeField] private float angleSpread = 5f;

    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.AddDecorator<MultiShotDecorator>(numberOfShots, angleSpread);
    }
}