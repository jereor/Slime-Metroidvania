using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public PlayerStateMachine Context { get; }
    public PlayerStateFactory PlayerStateFactory { get; }

    public override void EnterState()
    {
        Debug.Log("JUMP");
        HandleJump();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    void HandleJump()
    {
        _context.JumpButtonPressedTime = Time.time;

        bool isCoyoteTime = Time.time - _context.LastGroundedTime <= _context.CoyoteTime;
        bool isJumpBuffered = Time.time - _context.JumpButtonPressedTime <= _context.CoyoteTime;

        if (isCoyoteTime && isJumpBuffered)
        {
            _context.RigidBody.velocity = new Vector2(_context.RigidBody.velocity.x, _context.JumpForce);
        }
    }
}
