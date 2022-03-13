using System;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public PlayerStateFactory PlayerStateFactory { get; }


    // ENTER STATE
    public override void EnterState()
    {
        Debug.Log("JUMP");
        JumpStart();
    }

    void JumpStart()
    {
        Context.JumpButtonPressedTime = Time.time;

        Debug.Log("LastGroundedTime " + Context.LastGroundedTime);
        bool isCoyoteTime = Time.time - Context.LastGroundedTime <= Context.CoyoteTime;
        bool isJumpBuffered = Time.time - Context.JumpButtonPressedTime <= Context.CoyoteTime;

        //Debug.Log("isCoyoteTime " + isCoyoteTime + ", isJumpBuffered " + isJumpBuffered);

        if (isCoyoteTime && isJumpBuffered)
        {
            Debug.Log("Jump started!");
            Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.JumpForce);
        }
    }


    // EXIT STATE
    public override void ExitState()
    {
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
        Debug.Log("Start falling!");
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.RigidBody.velocity.y * 0.5f);
        Context.JumpButtonPressedTime = null;
        Context.LastGroundedTime = null;
    }


    // INITIALIZE SUB STATE
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    

    // CHECK SWITCH STATES
    public override void CheckSwitchStates()
    {
        if (Context.IsGrounded 
            && Context.IsJumpPressed == false)
        {
            SwitchState(Factory.Grounded());
        }
    }
}
