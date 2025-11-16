using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    //nếu gặp vực thẳm hoặc tưong thì sẽ quay đầu
    public override void Enter()
    {
        base.Enter();

        if (enemy.groundDetected == false || enemy.wallDetected)
            enemy.Flip();
    }
    public override void Update()
    {
        base.Update();
        
        enemy.SetVelocity(enemy.movementSpeed * enemy.facingDir, rb.linearVelocity.y);

        //nếu gặp tường hoặc vực thẳm thì sẽ vào trạng thái đứng yên

        if (enemy.groundDetected == false || enemy.wallDetected)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
