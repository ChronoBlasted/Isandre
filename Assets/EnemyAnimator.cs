using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    public void UpdateStateToWalk()
    {
        enemy.changeStateToMove();
    }

    public void UpdateStateToAttack()
    {
        enemy.Attack();
    }
}
