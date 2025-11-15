using UnityEngine;

public class PLayer_DashState : EntityState
{
    private float originalGravityScale;
    private int dashDir;
    public PLayer_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // hướng dash dựa trên input, nếu không có input thì dash theo hướng đang quay mặt
        dashDir = player.moveInput.x != 0 ? (int)(player.moveInput.x) : player.facingDir; 
        stateTimer = player.dashDurtaion; // thiết lập thời gian trạng thái bằng thời gian dash
        originalGravityScale = rb.gravityScale;   // lưu lại giá trị gravityScale ban đầu
        rb.gravityScale = 0; // tắt trọng lực trong khi dash
    }


    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * dashDir, 0f);
        CancelDashIfNeeded();

        if (stateTimer < 0)
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }   
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);    
        rb.gravityScale = originalGravityScale; // khôi phục lại giá trị gravityScale ban đầu
    }

    private void CancelDashIfNeeded()
    {
        if((player.wallDetected))
        {
            if (player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
    }

}
