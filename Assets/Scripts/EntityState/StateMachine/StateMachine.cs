using UnityEngine;

public class StateMachine
{
    public EntityState currentState { get; private set; }
    public bool canChangeState;

    /* Các trạng thái của nhân vật sẽ được quản lý bởi StateMachine, các phương thức Enter
    , Update(), Exit(), Enter() sẽ được gọi từ EntityState.cs */

    public void Initialize(EntityState startingState)
    {
        canChangeState = true;  
        currentState = startingState; // khởi tạo trạng thái hiện tại của nhân vật
        currentState.Enter(); // gọi phương thức Enter() của trạng thái hiện tại
    }
    public void ChangeState(EntityState newState)
    {
        if(!canChangeState) return; // nếu không thể thay đổi trạng thái thì thoát khỏi phương thức
        currentState.Exit(); // thoát khỏi trạng thái hiện tại
        currentState = newState; // chuyển sang trạng thái mới
        currentState.Enter(); // gọi phương thức Enter() của trạng thái mới
    }

    public void UpdateActiveState()
    {
        currentState.Update(); // gọi phương thức Update() của trạng thái hiện tại
    }

    public void SwitchOffStateMachine()
    {
        canChangeState = false; // tắt khả năng thay đổi trạng thái
    }

}
