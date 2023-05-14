using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class HeavyAttack : PlayerAction
{
    [SerializeField] private Sword sword;
    [SerializeField] private Animator playerAnimator;

    protected override void OnActionPerformed(CallbackContext context)
    {
        if (sword.InHand && !sword.DrawingSword && !sword.Attacking)
            StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        sword.Attacking = true;
        playerAnimator.SetBool("LightAttack", false);
        playerAnimator.SetTrigger("SwordSlash");
        yield return new WaitUntil(() => Utility.AnimationFinished(playerAnimator, "HeavySlash", 1));
        playerAnimator.SetTrigger("SwordSlashFinished");
        sword.Attacking = false;
    }
}
