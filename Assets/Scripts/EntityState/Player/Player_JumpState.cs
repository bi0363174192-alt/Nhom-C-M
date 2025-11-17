using UnityEditor.Tilemaps;
using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, player.jumpForce );
    }

    public override void Update()
    {
        base.Update();

        // chuyển sang trạng thái rơi khi đang rơi xuống và không bấm nút đánh thường
        if (rb.linearVelocity.y < 0 && stateMachine.currentState != player.jumpAttackState)
            stateMachine.ChangeState(player.fallState);
    }
}
