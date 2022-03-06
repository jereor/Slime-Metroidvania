using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public PlayerStateMachine Context { get; }
    public PlayerStateFactory PlayerStateFactory { get; }

    public override void EnterState()
    {
        Debug.Log("RUN");
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }
}
