using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DrawSword : PlayerAction
{
    [SerializeField] private Sword sword;
    [SerializeField] private Animator playerAnimator;

    protected override void OnActionStarted(CallbackContext context)
    {
        StartCoroutine(SwordAnimation());
    }

    private IEnumerator SwordAnimation()
    {
        sword.DrawingSword = true;
        playerAnimator.SetTrigger("DrawSword");
        yield return new WaitUntil(() => Utility.AnimationFinished(playerAnimator, "DrawSword", 1));
        sword.InHand = !sword.InHand;
        playerAnimator.SetBool("SwordInHand", sword.InHand);
        playerAnimator.SetTrigger("DrawSwordReverse");
        yield return new WaitUntil(() => Utility.AnimationFinished(playerAnimator, "DrawSwordReverse", 1));
        sword.DrawingSword = false;
        playerAnimator.SetTrigger("DrawSwordFinished");
    }
}
