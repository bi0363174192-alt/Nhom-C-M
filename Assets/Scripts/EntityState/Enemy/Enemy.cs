using Unity.Cinemachine;
using UnityEngine;

public class Enemy : Entity
{

    //quản lí các trạng thái của quái vật

    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;

    //các thông số cần thiết cho quái vật
    [Header("Battle details")]
    public float battleMoveSpeed = 3f;
    public float attackRange = 2f;
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 1;
    public Vector2 retreatVelocity;



    [Header("Movement details")]
    public float movementSpeed = 1.4f;
    public float idleTime = 1;
    [Range(0,2)]
    public float moveAnimSpeedMultiplier = 1;


    // các biến để kiểm tra player 
    [Header("Player detection")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10;
    public Transform player { get; private set; }


    public override void EntityDeath()
    {
        base.EntityDeath();
        stateMachine.ChangeState(deadState);
    }

    public void TryEnterBattleState(Transform player) // Hàm kiếm vị trí của player và chuyển sang trạng thái chiến đấu
    {
        if (stateMachine.currentState == battleState || stateMachine.currentState == attackState) return; // nếu đang ở trạng thái chiến đấu hoặc tấn công thì thoát khỏi hàm

        this.player = player;
        stateMachine.ChangeState(battleState); 
    }

    public Transform GetPlayerReference() //  Hàm kiếm vị trí của player khi không phát hiện được player ở trước mặt
    {
        if (player == null)
            player = PlayerDetection().transform;

        return player;
    }


    // sử dụng raycast bắn 1 tia từ quái vật để kiểm tra player hoặc là chướng ngại vật
    public RaycastHit2D PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, playerMask | whatIsGround);

        //nếu không xảy ra va chạm hoặc va chạm không phải là player thì trả về default

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;
        //nếu chạm phải player thì  trả về thông tin va chạm
        return hit;

    }

    //vẻ 1 đường thẳng để trực quan hóa phạm vi tấn công của quái vật và tầm đánh của nó
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * attackRange), playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y));
    }


}
