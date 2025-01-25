using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData WeaponData;
    public Transform firePoint;
    public AttackBehaviour attackBehaviour;
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

            timeSinceLastAttack = 0f;
        }
    }
}
