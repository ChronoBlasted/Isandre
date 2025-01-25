using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData WeaponData;
    public Transform firePoint;
    public AttackBehaviour attackBehaviour;
    public ParticleSystem ps;
    public LayerMask layerToAttack;

    float timeSinceLastAttack = 0f;

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void Fire()
    {
        if (timeSinceLastAttack >= (1f / WeaponData.attackRate))
        {
            attackBehaviour.Attack(this);

            if (ps != null) ps.Play();

            CameraManager.Instance.ShakeCamera();

            timeSinceLastAttack = 0f;
        }
    }
}
