using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Raffale", order = 1)]
public class Raffale : PowerUp
{
    [SerializeField] private int burstCount = 2;
    [SerializeField] private float timeBetweenShots = 0.5f;

    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.AddDecorator<BurstFireDecorator>(burstCount, timeBetweenShots);
    }
}