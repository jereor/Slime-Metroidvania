using Player.Core_Components;
using Player.State_Machine.States;

namespace Player.State_Machine
{
    // TODO: Make this a super state?
    public class PlayerKnockedBackState : PlayerBaseState
    {
        private PlayerController _playerController;
        private PlayerAnimations _playerAnimations;
        private PlayerMovement _playerMovement;

        private PlayerController PlayerController
        {
            get { return _playerController ??= Core.GetCoreComponent<PlayerController>(); }
        }
        
        private PlayerAnimations PlayerAnimations
        {
            get { return _playerAnimations ??= Core.GetCoreComponent<PlayerAnimations>(); }
        }
        
        private PlayerMovement PlayerMovement
        {
            get { return _playerMovement ??= Core.GetCoreComponent<PlayerMovement>(); }
        }

        public PlayerKnockedBackState(PlayerBase player, PlayerStateMachine context, PlayerStateFactory playerStateFactory)
            : base (player, context, playerStateFactory)
        {
        }

        protected override void EnterState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsKnockedBackHash, true);
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
        }

        protected override void ExitState()
        {
            PlayerAnimations.SetAnimatorBool(PlayerAnimations.IsKnockedBackHash, false);
        }

        protected override void CheckSwitchStates()
        {
            if (PlayerMovement.IsGrounded())
            {
                if (PlayerController.IsMovementInputPressed)
                {
                    SwitchState(Factory.Move());
                }
                else
                {
                    SwitchState(Factory.Idle());   
                }
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}