using UnityEngine;

public class Player_DeadState : PlayerState
{
    private GameManager gameManager;
    public Player_DeadState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    override public void Enter()
    {
        base.Enter();
        input.Disable(); // Vô hiệu hóa tất cả các input của người chơi khi chết
        rb.simulated = false; // Tắt mô phỏng vật lý để nhân vật không bị ảnh hưởng bởi vật lý
        gameManager = player.GetComponentInParent<GameManager>();
        gameManager?.GameOver(); // Gọi hàm GameOver từ GameManager khi nhân vật chết
    }


}
