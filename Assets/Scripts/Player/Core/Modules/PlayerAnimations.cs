using Player.Core.Parameters;
using UnityEngine;
using Utility;

namespace Player.Core.Modules
{
    public class PlayerAnimations
    {
        private readonly PlayerCombat _playerCombat;
        private readonly Animator _animator;

        public PlayerAnimations(PlayerAnimationsParameters playerAnimationsParameters)
        {
            _playerCombat = playerAnimationsParameters.PlayerCombat;
            _animator = playerAnimationsParameters.Animator;

            InitializeHashes();
        }

        // Animator hashes
        public int IsMovingHash { get; private set; }
        public int IsAirborneHash { get; private set; }
        public int IsMeleeAttackingHash { get; private set; }

        public void InitializeHashes()
        {
            IsMovingHash = Animator.StringToHash(AnimatorConstants.IS_MOVING);
            IsAirborneHash = Animator.StringToHash(AnimatorConstants.IS_AIRBORNE);
            IsMeleeAttackingHash = Animator.StringToHash(AnimatorConstants.IS_MELEE_ATTACKING);
        }
        
        public void SetAnimatorBool(int animatorHash, bool value)
        {
            _animator.SetBool(animatorHash, value);
        }
        
        // Animation events
        public void FinishAttack()
        {
            _playerCombat.DealDamage();
            _playerCombat.FinishMeleeAttacking();
        }
    }
}
