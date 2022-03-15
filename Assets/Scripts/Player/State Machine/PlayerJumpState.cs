using System;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public PlayerStateFactory PlayerStateFactory { get; }


    // ENTER STATE
    public override void EnterState()
    {
        Context.Animator.SetBool(Context.IsAirborneHash, true);
        JumpStart();
    }

    void JumpStart()
    {
        if (Context.IsGrounded == false)
        {
            return;
        }

        Context.JumpButtonPressedTime = Time.time;

        bool isCoyoteTime = Time.time - Context.LastGroundedTime <= Context.CoyoteTime;
        bool isJumpBuffered = Time.time - Context.JumpButtonPressedTime <= Context.CoyoteTime;

        if (isCoyoteTime && isJumpBuffered)
        {
            Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.JumpForce);
        }
    }


    // EXIT STATE
    public override void ExitState()
    {
        Context.Animator.SetBool(Context.IsAirborneHash, false);
    }


    // UPDATE STATE
    public override void UpdateState()
    {
        CheckSwitchStates();
        CheckJumpEnd();
    }

    private void CheckJumpEnd()
    {
        if (Context.RigidBody.velocity.y > 0f
            && Context.IsJumpPressed == false)
        {
            StartFalling();
        }
    }

    private void StartFalling()
    {
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.RigidBody.velocity.y * 0.5f);
        Context.JumpButtonPressedTime = null;
        Context.LastGroundedTime = null;
    }


    // INITIALIZE SUB STATE
    public override void InitializeSubState()
    {
    }
    

    // CHECK SWITCH STATES
    public override void CheckSwitchStates()
    {
        if (Context.IsJumpPressed == false)
        {
            SwitchState(Factory.Grounded());
        }
    }
}
