using UnityEngine;

public class EnemyMoveState : State<Enemy>
{
    public override void Enter()
    {
        if ((_owner.target.position - _owner.transform.position).magnitude > _owner.enemyData.enemyRange)
            _owner.AnimationChange("Walk");
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
        //PoolManager.Instance[ResourceType.Player].Get();
        
        //PoolManager.Instance[ResourceType.None].Release(gameObject);



        if ((_owner.target.position - _owner.transform.position).magnitude <= _owner.enemyData.enemyRange)
        {
            _owner.ChangeStateToAttack();
            return;
        }

        Vector3 dir = (_owner.target.position - _owner.transform.position).normalized;
        _owner.transform.position += dir * _owner.enemyData.ennemyspeed * Time.deltaTime;

    }

}
