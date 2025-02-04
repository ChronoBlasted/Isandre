using NUnit.Framework;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    Weapon CreateWeapon()
    {
        return new Weapon();
    }

    [Test]
    public void ShotgunTest()
    {
        Weapon weapon = CreateWeapon();

        weapon.WeaponData = (WeaponData)ResourceObjectHolder.Instance.GetResourceByType(ResourceType.Shotgun);
        weapon.attackBehaviour = new SpreadRandomShootBehaviour();
    }

    [Test]
    public void KickTest()
    {
        Weapon weapon = CreateWeapon();

        weapon.WeaponData = (WeaponData)ResourceObjectHolder.Instance.GetResourceByType(ResourceType.Kick);
        weapon.attackBehaviour = new KickAttackBehaviour();
    }

    [Test]
    public void PunchTest()
    {
        Weapon weapon = CreateWeapon();

        weapon.WeaponData = (WeaponData)ResourceObjectHolder.Instance.GetResourceByType(ResourceType.Punch);
        weapon.attackBehaviour = new PunchAttackBehaviour();
    }

    [Test]
    public void Riffle()
    {
        Weapon weapon = CreateWeapon();

        weapon.WeaponData = (WeaponData)ResourceObjectHolder.Instance.GetResourceByType(ResourceType.AssaultRifle);
        weapon.attackBehaviour = new DefaultShootBehaviour();
    }

    [Test]
    public void Pistol()
    {
        Weapon weapon = CreateWeapon();

        weapon.WeaponData = (WeaponData)ResourceObjectHolder.Instance.GetResourceByType(ResourceType.Pistol);
        weapon.attackBehaviour = new DefaultShootBehaviour();
    }
}
