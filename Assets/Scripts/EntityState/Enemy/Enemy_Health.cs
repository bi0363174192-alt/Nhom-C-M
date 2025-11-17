using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public override bool TakeDamage(float damage, Transform damageDealer)
    {
        bool wasHit = base.TakeDamage(damage, damageDealer);
        if (wasHit == false)
            return false;

        if (damageDealer.CompareTag("Player")) // Hàm check người tấn công và cho enemy tấn công nếu còn tồn tại 
            enemy.TryEnterBattleState(damageDealer);
        return true;
    }

}
