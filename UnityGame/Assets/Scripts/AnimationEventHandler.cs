using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private void OnFootstep(AnimationEvent animationEvent)
    {
        AudioManager.Instance.PlayOneShotWithParameters(FMODEvents.Instance.Footstep, transform.position, "Surface", 1);
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        // TODO land sound
    }
}
