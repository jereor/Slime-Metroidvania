using Player.State_Machine.States;

namespace Player.State_Machine
{
    public class PlayerStateFactory
    {
        private readonly PlayerStateMachine _context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
        }

        public PlayerBaseState Idle()
        {
            return new PlayerIdleState(_context, this);
        }

        public PlayerBaseState Move()
        {
            return new PlayerMoveState(_context, this);
        }

        public PlayerBaseState MeleeAttack()
        {
            return new PlayerMeleeAttackState(_context, this);
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
}
