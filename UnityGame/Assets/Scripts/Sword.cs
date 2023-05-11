using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject handTarget;
    [SerializeField] private GameObject hipTarget;
    [SerializeField] private GameObject offsetPoint;

    private bool inHand = false;
    private bool attacking = false;
    private bool drawingSword = false;

    private Vector3 inHandPosOffset = new(-0.06f, 0.09f, 0.01f);
    private Vector3 inHandRotOffset = new(-90f, 90f, 0f);

    private Vector3 onHipPosOffset = new(-0.18f, 0f, 0f);
    private Vector3 onHipRotOffset = new(-160f, 0f, 10f);

    public void ToggleSword(Animator animator)
    {
        StartCoroutine(SwordAnimation(animator));
    }

    public void LightAttack(Animator animator)
    {
        if (inHand && !drawingSword && !attacking)
            StartCoroutine(AttackAnimation(animator, true));
    }

    public void HeavyAttack(Animator animator)
    {
        if (inHand && !drawingSword && !attacking)
            StartCoroutine(AttackAnimation(animator, false));
    }

    public void OnChildTriggerEnter(Collider other)
    {
        if (attacking && other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Hit(1f);
        }
    }

    private IEnumerator SwordAnimation(Animator animator)
    {
        drawingSword = true;
        animator.SetTrigger("DrawSword");
        yield return new WaitUntil(() => Utility.AnimationFinished(animator, "DrawSword", 1));
        inHand = !inHand;
        animator.SetBool("SwordInHand", inHand);
        animator.SetTrigger("DrawSwordReverse");
        yield return new WaitUntil(() => Utility.AnimationFinished(animator, "DrawSwordReverse", 1));
        drawingSword = false;
        animator.SetTrigger("DrawSwordFinished");
    }

    private IEnumerator AttackAnimation(Animator animator, bool isLight)
    {
        attacking = true;
        animator.SetBool("LightAttack", isLight);
        animator.SetTrigger("SwordSlash");
        yield return new WaitUntil(() => Utility.AnimationFinished(animator, isLight ? "LightSlash" : "HeavySlash", 1));
        animator.SetTrigger("SwordSlashFinished");
        attacking = false;
    }

    private void Update()
    {
        if (inHand)
        {
            transform.SetPositionAndRotation(handTarget.transform.position, handTarget.transform.rotation);
            offsetPoint.transform.localPosition = inHandPosOffset;
            offsetPoint.transform.localRotation = Quaternion.Euler(inHandRotOffset);
        }
        else
        {
            transform.SetPositionAndRotation(hipTarget.transform.position, hipTarget.transform.rotation);
            offsetPoint.transform.localPosition = onHipPosOffset;
            offsetPoint.transform.localRotation = Quaternion.Euler(onHipRotOffset);
        }
    }
}
