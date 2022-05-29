using Player.State_Machine.States;

namespace Player.State_Machine
{
    public class PlayerStateFactory
    {
        private readonly Core_Components.Player _player;
        private readonly PlayerStateMachine _context;

        public PlayerStateFactory(Core_Components.Player player, PlayerStateMachine currentContext)
        {
            _player = player;
            _context = currentContext;
        }

        public PlayerBaseState Idle()
        {
            return new PlayerIdleState(_player, _context, this);
        }

        public PlayerBaseState Move()
        {
            return new PlayerMoveState(_player, _context, this);
        }

        public PlayerBaseState MeleeAttack()
        {
            return new PlayerMeleeAttackState(_player, _context, this);
        }

        public PlayerBaseState Jump()
        {
            return new PlayerJumpState(_player, _context, this);
        }

        public PlayerBaseState Grounded()
        {
            return new PlayerGroundedState(_player, _context, this);
        }
    }
}
