using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    public enum PLAYER_ANIMATION_PARAMETER
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


    public string GetCurrentAnimationName()
    {
        var animatorinfo = animator.GetCurrentAnimatorClipInfo(0);

        var current_animation = animatorinfo[0].clip.name;

        return current_animation;
    }
}
