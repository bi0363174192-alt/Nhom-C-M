using System.Collections;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using UnityEngine;

/* Player script đóng vai trò quản lý và kết nối các trạng thái khác nhau của nhân vật người chơi trong trò chơi
 Đồng thời cung cấp các thuộc tính và phương thức cần thiết để các trạng thái này hoạt động hiệu quả*/
public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    public PlayerInputSet input { get; private set; } // Lưu trữ tập hợp các input của người chơi
    private StateMachine stateMachine; // Khởi tạo StateMachine để quản lý các trạng thái của nhân vật


    // Các trạng thái của nhân vật người chơi
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public PLayer_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }


    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetTime = 1;
    private Coroutine queuedAttackCo;


    [Header("Movement Stats")]
    public float moveSpeed = 3;
    public float junpForce = 5;
    public Vector2 wallJumpForce;

    [Range(0, 1)]
    public float inAirMoveMultiplier = .7f;
    [Range(0, 1)]
    public float wallSlideSlowMultiplier = .7f;
    [Space]
    public float dashDurtaion = .25f;
    public float dashSpeed = 20;


    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;
    public Vector2 moveInput { get; private set; }


    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }




    private void Awake()
    {
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        // Khởi tạo các trạng thái và truyền tham số vào hàm được khởi tạo ở EntityState.cs
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new PLayer_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");

    }


    private void OnEnable()
    {
        // Kích hoạt hệ thống input và lưu trữ giá trị di chuyển vào biến moveInput
        // 
        input.Enable();
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }


    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        // Khởi tạo trạng thái ban đầu là đứng yên
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState(); // Cập nhật trạng thái hiện tại trong StateMachine mỗi khung hình
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCo != null) //Nếu đã có Coroutine đang chạy thì dừng chạy và thực hiện nhất quán Coroutine trước đó
            StopCoroutine(queuedAttackCo);
        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        // Chờ đến hết khung hình của trạng thái trước đó mới chuyển sang trạng thái tấn công
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    public void CallAnimationTrigger()
    {
        // Gọi hàm CallAnimationTrigger trong EntityState.cs từ StateMachine.cs
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }


    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
            Flip();
        else if (xVelocity < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        // Kiểm tra va chạm với mặt đất và tường bằng raycast
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                    && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    }

    private void OnDrawGizmos()
    {
        // Vẽ các đường raycast để kiểm tra va chạm trong chế độ chỉnh sửa
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));

    }

}
