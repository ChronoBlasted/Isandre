using UnityEngine;

public class EnemyDieState : State<Enemy>
{
    
    public override void Enter()
    {
        _owner.animator.SetTrigger("Die");
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
