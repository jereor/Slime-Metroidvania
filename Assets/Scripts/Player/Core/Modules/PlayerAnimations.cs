using Player.Core.Parameters;
using UnityEngine;
using Utility;

namespace Player.Core.Modules
{
    public class PlayerAnimations
    {
        private readonly PlayerCombat _playerCombat;

        public PlayerAnimations(PlayerAnimationsParameters playerAnimationsParameters)
        {
            _playerCombat = playerAnimationsParameters.PlayerCombat;

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
        
        // Animation events
        public void FinishAttack()
        {
            _playerCombat.DealDamage();
            _playerCombat.FinishMeleeAttacking();
        }
    }
}
