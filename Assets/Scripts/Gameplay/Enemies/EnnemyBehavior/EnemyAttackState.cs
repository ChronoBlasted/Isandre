using UnityEngine;

public class EnemyAttackState : State<Enemy>
{

    public override void Enter()
    {
        _owner.AnimationChange("Attack");        
    }

    public override void Exit()
    {
    }

    public override void LateUpdate()
    {
    }

    public override void Update()
    {
    }
}
