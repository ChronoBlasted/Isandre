
using UnityEngine;

public class EnemyMelee : Enemy
{
    public float attackRadius = 1;
    public int damage;
    public override void Awake()
    {
        base.Awake();

        inAction = false;
    }

    #region Actions
    public override void Attack()
    {
        base.Attack();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position , attackRadius, LayerMask.GetMask("Player"));
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Alive>().ChangeLife(-damage);
        }
    }    
    //protected override void Die()
    //{
    //    base.Die();
    //}
    //protected override void Move()
    //{
    //    Vector3 dir = (target.position - transform.position).normalized;
    //    transform.position += dir * enemyData.enemyRange * Time.deltaTime;
    //}
    //#endregion

    //private void Update()
    //{
    //    if (!inAction)
    //    {
    //        SetState();

    //        switch (STATE)
    //        {
    //            case STATE_MACHINE.IDLE:
    //                break;
    //            case STATE_MACHINE.MOVING:
    //                Move();
    //                break;
    //            case STATE_MACHINE.ATTACK:
    //                Attack();
    //                break;
    //        }
    //    }

    //    if (hpScript.GetLife()<=0)
    //    {
    //        Die();
    //    }
    //}
    #endregion
}