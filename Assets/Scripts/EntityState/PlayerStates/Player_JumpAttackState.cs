using UnityEngine;

public class Player_JumpAttackState  : PlayerState  
{

    private bool touchedGround;

    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        touchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y );
    }


    public override void Update()
    {
        base.Update();

        if(player.groundDetected && !touchedGround) // khi đang ở trạng thái nhảy đánh mà chạm đất lần đầu
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if(triggerCalled && player.groundDetected)  // khi kết thúc animation và đã chạm đất
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
