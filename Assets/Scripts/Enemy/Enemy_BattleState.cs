using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    //đây là trạng thái chiến đấu của quái vật điền khiển tất cả các hành động tấn công của quái vật
    private Transform player;
    private float lastTimeWasInBattle;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    // enemy vào trạng thái chiến đấu nếu có player trong phạm vi tấn công  
    public override void Enter()
    {
        base.Enter();

        if (player == null)
            player = enemy.PlayerDetection().transform;
        //neu can giat lui lai thi lam cho quai vat giat lui lai truoc khi tan cong de tranh bug

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }

    }



    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetection() == true)
            // cap nhat thoi gian trong trang thai chien dau
            UpdateBattleTimer();
        //neu thoi gian trong trang thai chien dau ket thuc thi chuyen ve trang thai dung yen
        if (BattleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);
        //neu trong tam danh thi vao trang thai tan cong 
        if (WithinAttackRange() && enemy.PlayerDetection())
            stateMachine.ChangeState(enemy.attackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }


    //thoi gian trong trang thai chien dau
    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;

    private bool BattleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;
    //kiểm tra xem player có nằm trong tầm đánh của enemy hay không
    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackRange;


    //kiem tra xem co nen giat lui lai ko
    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;


    // kiểm tra khoảng cách từ quái vật đến player 
    private float DistanceToPlayer()
    {
        if (player == null)
        {
            return float.MaxValue;
        }
        else return Mathf.Abs(player.position.x - enemy.transform.position.x);

    }

    private int DirectionToPlayer()
    {
        if (player == null)
        {
            return 0;
        }
        else return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}