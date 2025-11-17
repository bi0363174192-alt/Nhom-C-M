using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
=======
    private Entity_SFX sfx;
    private Entity_VFX vfx;
    public float damage = 10;
>>>>>>> Stashed changes
>>>>>>> Stashed changes
=======
    private Entity_SFX sfx;
    private Entity_VFX vfx;
    public float damage = 10;
>>>>>>> Stashed changes

    [Header("Targer dectection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 0.5f;
    [SerializeField] private LayerMask whatIsTarget;


<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
    public void PerformAttack(float damage)
=======
    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        sfx = GetComponent<Entity_SFX>();
    }

    public void PerformAttack()
>>>>>>> Stashed changes
    {
<<<<<<< Updated upstream
=======
=======
    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        sfx = GetComponent<Entity_SFX>();
    }

    public void PerformAttack()
    {
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        bool targetGoHit = false;
        foreach (var target in getDetectedColliders())  // Lặp qua tất cả các đối tượng bị phát hiện
        {
            IDamgable damgable = target.GetComponent<IDamgable>();
            damgable?.TakeDamage(damage, transform);

<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
            if (targerHealth != null) 
                targerHealth.TakeDamage(damage, transform);
            
=======
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes

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
        {
            sfx?.playAttackMiss();
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        }
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
