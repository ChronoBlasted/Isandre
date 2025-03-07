using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;

    public IAttackBehaviour attackBehaviour;

    public bool useMultiShot = false;    

    public ParticleSystem ps;
    public LayerMask layerToAttack;

    float timeSinceLastAttack = 0f;

    private void Start()
    {
        attackBehaviour = new DefaultShootBehaviour();       
    }
    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void Fire()
    {
        if (timeSinceLastAttack >= (1f / weaponData.attackRate))
        {
            attackBehaviour.Attack(this);

            if (ps != null) ps.Play();
            AudioManager.Instance.PlaySound(weaponData.audioClipName);
            CameraManager.Instance.ShakeCamera();

            timeSinceLastAttack = 0f;
        }
    }
}
