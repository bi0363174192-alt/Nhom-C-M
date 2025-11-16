using System;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVeloctyTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued;
    private int attackDir;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private const int FirstComboIndex = 1   ;  


    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (player.attackVelocity.Length != comboLimit) // điều chỉnh lại độ dài combo attack
        {
            comboLimit = player.attackVelocity.Length;
        }

    }
    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false; // hàng đợi của đòn đánh tiếp theo mặc định là false
        ResetComboIndexIfNeeded();

        //Xác định hướng tấn công dựa theo x input
        attackDir = player.moveInput.x != 0 ? (int)(player.moveInput.x) : player.facingDir;


        anim.SetInteger("basicAttackIndex", comboIndex); 
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPerformedThisFrame())
            QueueNextAttack();

        if (triggerCalled) 
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
    }


    private void HandleStateExit() // hàm này xử lý khi người dùng spam liên tục attack
    { 
        if (comboAttackQueued) 
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {

        if(comboIndex < comboLimit)
            comboAttackQueued = true;
    }

    private void HandleAttackVelocity() // hàm này xử lý vận tốc tấn công
    {
        attackVeloctyTimer -= Time.deltaTime;
        if(attackVeloctyTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y); 
    }

    private void ApplyAttackVelocity() // hàm nào áp dụng vận tốc tấn công cho mỗi đòn đánh
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];

        attackVeloctyTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded() // hàm này xử lý nếu quá thời gian giữa các đòn đánh thì combo sẽ reset về 1
    {
        if(Time.time > lastTimeAttacked + player.comboResetTime || comboIndex > comboLimit) 
            comboIndex = FirstComboIndex;
    }
}
