using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity_AnimationTrigger : MonoBehaviour
{

    private Entity entity;
    private Entity_Combat entityCombat;

    private void Awake()
    {
        entityCombat = GetComponentInParent<Entity_Combat>();
        entity = GetComponentInParent<Entity>();
    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();

    }
    private void AttackTrigger()
    {
        entityCombat.PerformAttack();
    }
}
