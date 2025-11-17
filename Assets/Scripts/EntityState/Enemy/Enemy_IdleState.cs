using UnityEngine;

public class Enemy_IdleState : Enemy_GroundedState
{
    public Enemy_IdleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    //đếm thời gian quái vật đứng yên
    public override void Enter()
    {
        base.Enter();


        stateTimer = enemy.idleTime;
    }
    //nếu biến đếm < 0 thì quái vật bắt đầu di chuyển 
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
