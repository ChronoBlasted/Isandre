using UnityEngine;

public class EntityAnimationTrigger : MonoBehaviour
{
    public Enemy enemy;

    public void TriggerAttack()
    {
        enemy.Attack();
    }
}
