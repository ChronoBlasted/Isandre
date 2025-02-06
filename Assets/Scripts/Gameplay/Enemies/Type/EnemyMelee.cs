using UnityEngine;
using System;
using System.Collections;
using Unity.Collections;

public class EnemyMelee : Enemy
{
    public override void Awake()
    {
        base.Awake();

        inAction = false;
    }

    #region Actions
    //protected override void Attack()
    //{
    //    base.Attack();
    //}    
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