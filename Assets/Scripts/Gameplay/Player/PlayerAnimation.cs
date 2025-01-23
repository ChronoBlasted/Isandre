using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    public enum PLAYER_ANIMATION
    {
        NONE,
        IDLE,
        WALK,
        DASH,
        RUN,
        DIE,
        HOLDING_RIGHT,
        SHOOT_RIGHT,
        PICK_UP,
    }

    public void PlayAnimation(PLAYER_ANIMATION animationName)
    {
        animator.Play(animationName.ToString());
    }
}
