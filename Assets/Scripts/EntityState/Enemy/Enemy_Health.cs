using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(float damage, Transform damageDealer)
    {
        if (damageDealer.CompareTag("Player")) // Hàm check người tấn công và cho enemy tấn công nếu còn tồn tại enemy
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TryEnterBattleState(damageDealer);
            }
        }
        base.TakeDamage(damage, damageDealer);
    }

}
