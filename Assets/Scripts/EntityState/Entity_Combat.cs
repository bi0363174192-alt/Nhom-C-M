using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    private Entity_SFX sfx;
    private Entity_VFX vfx;
    public float damage = 10;

    [Header("Targer dectection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 0.5f;
    [SerializeField] private LayerMask whatIsTarget;
    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        sfx = GetComponent<Entity_SFX>();
    }

    public void PerformAttack(float damage)
    {
        bool targetGoHit = false;
        foreach (var target in getDetectedColliders())  // Lặp qua tất cả các đối tượng bị phát hiện
        {
            IDamgable damgable = target.GetComponent<IDamgable>();
            damgable?.TakeDamage(damage, transform);

            targetGoHit = damgable.TakeDamage(damage, transform);
            if (damgable == null)
                continue;
            if (targetGoHit)
            {
                vfx.CreateOnHitVFX(target.transform);
                sfx?.playAttackHit();
            }


        }
        if (targetGoHit == false)
            sfx?.playAttackMiss();
    }

    private Collider2D[] getDetectedColliders() // Hàm trả về mảng các enemy được phát hiện bằng va chạm
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget); // check va chạm trong vùng tấn công  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius); // Vùng tấn công

    }
}
