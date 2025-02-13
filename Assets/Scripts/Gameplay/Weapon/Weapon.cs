using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;
    public AttackBehaviour attackBehaviour;
    public List<AttackBehaviour> otherBehaviour;
    public ParticleSystem ps;
    public LayerMask layerToAttack;

    float timeSinceLastAttack = 0f;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        otherBehaviour = new List<AttackBehaviour>();
        AttackBehaviour[] bhList = transform.GetComponentsInChildren<AttackBehaviour>() as AttackBehaviour[];
        for (int i = 0; i < bhList.Length; i++) 
        {
            otherBehaviour.Add(bhList[i]);
        }
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

            if(otherBehaviour.Count > 0)
            {
                foreach(AttackBehaviour behaviour in otherBehaviour)
                {
                    behaviour.Attack(this);
                }
                //Debug.Break();
            }

            if (ps != null) ps.Play();
            AudioManager.Instance.PlaySound(weaponData.audioClipName);

            CameraManager.Instance.ShakeCamera();

            timeSinceLastAttack = 0f;
        }
    }
}
