using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public float damge = 10;

    [Header("Targer dectection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 0.5f;
    [SerializeField] private LayerMask whatIsTarget;


    public void PerformAttack()
    {
        
        foreach (var target in getDetectedColliders())  // Lặp qua tất cả các đối tượng bị phát hiện
        {
            Entity_Health targerHealth = target.GetComponent<Entity_Health>(); // Lấy component Entity_Health từ đối tượng bị phát hiện

            if (targerHealth != null) 
                targerHealth.TakeDamage(damge, transform);
            
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
