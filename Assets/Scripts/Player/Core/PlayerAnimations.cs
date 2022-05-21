using JetBrains.Annotations;
using UnityEngine;
using Utility;

namespace Player.Core
{
    public class PlayerAnimations
    {
        private readonly PlayerAdapter _playerAdapter;

        public PlayerAnimations(PlayerAdapter playerAdapter)
        {
            _playerAdapter = playerAdapter;

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
        [UsedImplicitly]
        public void FinishAttack()
        {
            _playerAdapter.DealDamage();
            _playerAdapter.FinishMeleeAttacking();
        }
    }
}