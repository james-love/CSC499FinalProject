using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private void OnFootstep(AnimationEvent animationEvent)
    {
        AudioManager.Instance.PlayOneShotWithParameters(FMODEvents.Instance.PlayerFootstep, transform.position, "Surface", 1);
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        AudioManager.Instance.PlayOneShotWithParameters(FMODEvents.Instance.PlayerFootstep, transform.position, "Surface", 1);
    }
}
