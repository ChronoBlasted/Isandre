using UnityEngine;

public class EnemyHitState : State<Enemy>
{
    public override void Enter()
    {
        _owner.animator.Play("Hit", 1);
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
