using UnityEngine;

public class Player_IdleState : Player_GroundedState
{


    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0,rb.linearVelocity.y);
    }
    public override void Update()
    {
        base.Update();
        // Nếu nút bấm trên bàn phím cùng hướng với nhân vật và có tường bên cạnh thì không cho di chuyển
        if (player.moveInput.x == player.facingDir && player.wallDetected)
            return;
        
        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);

    }
}
