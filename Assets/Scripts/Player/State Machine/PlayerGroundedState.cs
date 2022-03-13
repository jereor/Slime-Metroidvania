using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }

    public PlayerStateFactory PlayerStateFactory { get; }

    // TODO: Figure out why EnterState is called every frame after the first jump
    public override void EnterState()
    {
        Debug.Log("GROUNDED");
    }

    public override void ExitState()
    {
        Context.LastGroundedTime = Time.time;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void InitializeSubState()
    {
        if (Context.IsMovementPressed)
        {
            SetSubState(Factory.Move());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Context.IsJumpPressed)
        {
            SwitchState(Factory.Jump());
        }
    }
}
