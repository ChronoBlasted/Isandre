using UnityEngine;

public class EnemyMoveState : State<Enemy>
{
    public override void Enter()
    {
        if ((_owner.target.position - _owner.transform.position).magnitude > _owner.enemyData.enemyRange)
            _owner.animator.SetTrigger("Walk");
        else
            _owner.ChangeStateToAttack();

    }

    public override void Exit()
    {
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        
        if ((_owner.target.position - _owner.transform.position).magnitude <= _owner.enemyData.enemyRange)
        {
            _owner.ChangeStateToAttack();
            return;
        }

        Vector3 dir = (_owner.target.position - _owner.transform.position).normalized;
        _owner.transform.position += dir * _owner.enemyData.enemyRange * Time.deltaTime;

    }

}
