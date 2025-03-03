using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/TireRate", order = 1)]
public class PowerUpTireRateIncrease : PowerUp
{
    [SerializeField] float attackRateMultiplier;
    public override void OnUse()
    {
        base.OnUse();
        PlayerManager.Instance.playerWeapon.currentWeapon.weaponData.attackRate = PlayerManager.Instance.playerWeapon.currentWeapon.weaponData.attackRate / attackRateMultiplier;
    }
}
