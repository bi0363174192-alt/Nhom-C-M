using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamgable  
{
    private Slider healthBar;
    private Entity_VFX entityVFX;
    private Entity entity;

    [SerializeField]  protected float currentHp;
    [SerializeField]  protected float maxHp = 100; 
    [SerializeField]  protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f,2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private float heavyKnockbackDuration = .4f;
    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = .3f; // Nếu đòn tấn công lấy đi 30% máu trở lên thì coi là đòn tấn công mạnh


    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();

        currentHp = maxHp;
        UpdateHealthBar();
    }


    public virtual bool TakeDamage(float damage, Transform damageDealer) // Transform damageDealer = để biết ai đã tấn công
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes

=======
        if (isDead) return false;
>>>>>>> Stashed changes
<<<<<<< Updated upstream
=======
=======
        if (isDead) return false;
>>>>>>> Stashed changes
>>>>>>> Stashed changes

        Vector2 knockback = CalculateKnockBack(damage,damageDealer);  // Tính toán lực đẩy lùi dựa trên sát thương và vị trí người tấn công
        float duration = CalculateDuration(damage);

        entity?.RecieveKnockBack(knockback, duration); // Chỉ gọi entity nếu không null
        entityVFX?.PlayOnDamageVFX();  // Chỉ gọi entityVFX nếu không null
        ReduceHp(damage);
        return true;
    }

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar();
        if (currentHp <= 0) {
            Die();
    }

}

    private void Die()
    {
        isDead = true;
        entity?.EntityDeath(); // Chỉ gọi entity nếu không null
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.value = currentHp / maxHp;
        else return;
    }
    private Vector2 CalculateKnockBack(float damage, Transform damagedealer)
    {
        int direction = transform.position.x > damagedealer.position.x ? 1 : -1; // Xác định hướng đẩy lùi dựa trên vị trí của damageDealer
        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower; // Nếu sát thương nặng thì dùng trước dấu : còn không thì dùng sau dấu :

        knockback.x *= direction; // Đẩy lùi

        return knockback;

    }

    private float CalculateDuration(float Damage) //Nếu là đòn đánh mạng thì trả về thời gian đòn đánh mạnh
    {
        if (IsHeavyDamage(Damage))

            return heavyKnockbackDuration;
        else 
            return knockbackDuration;

    }

    private bool IsHeavyDamage(float damage) //Hàm xác định đòn đánh mạnh
    {
        return damage >= (maxHp * heavyDamageThreshold);
    }

}