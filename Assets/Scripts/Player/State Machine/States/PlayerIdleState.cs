using Player.Core.Modules;

namespace Player.State_Machine.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        private readonly PlayerCombat _playerCombat;
        
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _playerCombat = currentContext.PlayerAdapter.PlayerCombat;
        }

        protected override void EnterState()
        {
        }

        protected override void ExitState()
        {
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void InitializeSubState()
        {
        }

        protected override void CheckSwitchStates()
        {
            if (_playerCombat.IsMeleeAttacking)
            {
                Logger.LogVerbose("Idle -> Melee");
                SwitchState(Factory.MeleeAttack());
            }
            else if (PlayerController.IsMovementPressed)
            {
                Logger.LogVerbose("Idle -> Move");
                SwitchState(Factory.Move());
            }
        }
    }
}
