using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();
        // chuyển sang trạng thái walljumpstate khi nhảy
        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);

        // nếu như không còn chạm tường thì chuyển sang trạng thái rơi
        if (!player.wallDetected)
            stateMachine.ChangeState(player.fallState)  ;

        /* chuyển sang trạng thái idlestate khi chạm đất và lật ngược player lại 
         vì khi rơi hay trượt xuống player sẽ quay mặt về hướng tường */
        if (player.groundDetected)
        { 
            stateMachine.ChangeState(player.idleState);
            player.Flip();
        }

    }

    // Hàm này đóng vai trò xử lý việc trượt tường của nhân vật 
    private void HandleWallSlide() 
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
         /* else ở đây có nghĩa là khi y>0 <=> người chơi bấm nút di chuyển lên trên.
            Khi đó ta sẽ làm chậm vận tốc rơi của nhân vật bằng cách nhân với hệ số wallSlideSlowMultiplier */
        else
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier);


    }

}
