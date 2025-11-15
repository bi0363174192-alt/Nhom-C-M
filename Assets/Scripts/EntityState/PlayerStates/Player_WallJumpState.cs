using UnityEngine;

public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        // Thực hiện wall jump theo hướng ngược lại với tường
        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }


    public override void Update()
    {
        base.Update();

        if(rb.linearVelocity.y < 0) 
            stateMachine.ChangeState(player.fallState);

        if (player.wallDetected)
            stateMachine.ChangeState(player.wallSlideState); 
    }
}
