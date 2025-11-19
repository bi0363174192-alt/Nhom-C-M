using Unity.IO.LowLevel.Unsafe;
#if UNITY_EDITOR
using UnityEditor.Profiling.Memory;
#endif
using UnityEngine;



public abstract class PlayerState : EntityState

{
    protected Player player;
    protected PlayerInputSet input;


    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;   

        anim = player.anim;
        rb = player.rb;
        input = player.input;   
    }
    public override void Update()
    {
        base.Update();

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);

    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

    }
    private bool CanDash()
    {
        // hàm này thiết lập không cho ta đập mặt vào tường và dash liên tục
        if(player.wallDetected)
            return false;
        if(stateMachine.currentState == player.dashState)
            return false;
        return true;
    }

}
