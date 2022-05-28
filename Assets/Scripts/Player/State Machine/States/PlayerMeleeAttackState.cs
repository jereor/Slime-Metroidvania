using Player.Core.Modules;

namespace Player.State_Machine.States
{
    public class PlayerMeleeAttackState : PlayerBaseState
    {
        private readonly PlayerCombat _playerCombat;

        public PlayerMeleeAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _playerCombat = currentContext.PlayerAdapter.PlayerCombat;
        }

        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMeleeAttackingHash, true);
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsMeleeAttackingHash, false);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void CheckSwitchStates()
        {
            if (_playerCombat.IsMeleeAttacking)
            {
                return;
            }

            Logger.LogVerbose("Melee -> Move/Idle");

            SwitchState(PlayerController.IsMovementPressed
                ? Factory.Move()
                : Factory.Idle());
        }

        protected override void InitializeSubState()
        {
        }
    }
}
