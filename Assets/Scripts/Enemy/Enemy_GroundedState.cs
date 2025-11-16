using UnityEngine;

public class Enemy_GroundedState : EnemyState
{
    public Enemy_GroundedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();


        //khi quai vat nhan thay duoc nguoi choi
        //state machine chuyen state thanh battle state
        if (enemy.PlayerDetection() == true)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
