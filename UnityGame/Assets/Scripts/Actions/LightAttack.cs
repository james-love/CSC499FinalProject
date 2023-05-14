using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class LightAttack : PlayerAction
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
        playerAnimator.SetBool("LightAttack", true);
        playerAnimator.SetTrigger("SwordSlash");
        yield return new WaitUntil(() => Utility.AnimationFinished(playerAnimator, "LightSlash", 1));
        playerAnimator.SetTrigger("SwordSlashFinished");
        sword.Attacking = false;
    }
}
