using UnityEngine;

public class Player_AnimationTrigger : MonoBehaviour
{

    private Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void CurrentStateTrigger()
    {
        player.CallAnimationTrigger();

    }
        
 
}
