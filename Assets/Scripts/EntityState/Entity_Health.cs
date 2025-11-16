using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entityVFX;
    [SerializeField]  protected float maxHp = 100; 
    [SerializeField]  protected bool isDead;


    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
    }


    public virtual void TakeDamage(float damage, Transform damageDealer) // Transform damageDealer = để biết ai đã tấn công
    {
        if (isDead) return;

        entityVFX?.PlayOnDamageVFX();
        ReduceHp(damage); 
    }

    protected void ReduceHp(float damage)
    {
        maxHp -= damage;

        if (maxHp <= 0) {
            Die();
    }

}

    private void Die()
    {
        isDead = true;
    }
}