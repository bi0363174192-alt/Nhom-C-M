using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity_AnimationTrigger : MonoBehaviour
{

    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();

    }
}
