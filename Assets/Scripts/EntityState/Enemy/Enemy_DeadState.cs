using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        anim.enabled = false; // Tắt animator để tránh xung đột với vật lý
        rb.gravityScale = 12; // Bật trọng lực để đối tượng rơi xuống
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15); // Đặt vận tốc ngang về 0 để đối tượng không di chuyển ngang

        enemy.GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa collider để tránh va chạm không mong muốn
        stateMachine.SwitchOffStateMachine();
    }

}
