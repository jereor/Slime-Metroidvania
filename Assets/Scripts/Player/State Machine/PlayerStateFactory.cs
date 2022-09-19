using Player.State_Machine.States;

namespace Player.State_Machine
{
    public class PlayerStateFactory
    {
        private readonly PlayerBase _player;
        private readonly PlayerStateMachine _context;

        public PlayerStateFactory(PlayerBase player, PlayerStateMachine currentContext)
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

        public PlayerBaseState Airborne()
        {
            return new PlayerAirborneState(_player, _context, this);
        }
        
        public PlayerBaseState Jump()
        {
            return new PlayerJumpState(_player, _context, this);
        }
        
        public PlayerBaseState Fall()
        {
            return new PlayerFallState(_player, _context, this);
        }

        public PlayerBaseState Grounded()
        {
            return new PlayerGroundedState(_player, _context, this);
        }

        public PlayerBaseState KnockedBack()
        {
            return new PlayerKnockedBackState(_player, _context, this);
        }
    }
}
