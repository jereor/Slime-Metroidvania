using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public PlayerStateFactory PlayerStateFactory { get; }

    public override void EnterState()
    {
        Debug.Log("JUMP");
        HandleJump();
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
        if (Context.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    void HandleJump()
    {
        Context.JumpButtonPressedTime = Time.time;

        bool isCoyoteTime = Time.time - Context.LastGroundedTime <= Context.CoyoteTime;
        bool isJumpBuffered = Time.time - Context.JumpButtonPressedTime <= Context.CoyoteTime;

        if (isCoyoteTime && isJumpBuffered)
        {
            Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.JumpForce);
        }
    }
}
