using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public PlayerStateFactory PlayerStateFactory { get; }

    public override void EnterState()
    {
        Debug.Log("MOVE");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    
    public override void CheckSwitchStates()
    {
        if (Context.IsMovementPressed == false)
        {
            SwitchState(Factory.Idle());
        }
    }
}
