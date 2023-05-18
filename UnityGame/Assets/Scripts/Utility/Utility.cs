using UnityEngine;

public static class Utility
{
    public static bool AnimationFinished(Animator animator, string animation, int layerIndex = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(animation) &&
            animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime >= 1;
    }
}
