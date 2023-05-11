using UnityEngine;

public class TestEnemy : Enemy
{
    private float totalHealth = 1f;
    public override void Hit(float damageValue)
    {
        totalHealth = Mathf.Clamp(totalHealth - damageValue, 0f, totalHealth);
        if (totalHealth == 0f)
            Destroy(gameObject);
    }
}
