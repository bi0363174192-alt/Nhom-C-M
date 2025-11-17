using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        // chuyển sang trạng thái idlestate khi chạm đất
        if (player.groundDetected)
            stateMachine.ChangeState(player.idleState);
        // chuyển sang trạng thái wallslidestate khi chạm tường
        if (player.wallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
