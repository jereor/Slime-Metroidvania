using System;
using Player.State_Machine;

public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    internal PlayerBaseState Move()
    {
        return new PlayerMoveState(_context, this);
    }
    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
}
