using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    //đây là trạng thái tấn công của quái vật

    public Enemy_AttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();


        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
