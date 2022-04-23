using JetBrains.Annotations;
using Player.State_Machine;
using UnityEngine;
using Utility;

namespace Player.Core
{
    public class PlayerAnimations
    {
        private readonly IStateMachine _context;
        private Animator _animator;

        public PlayerAnimations(IStateMachine context)
        {
            _context = context;
            _animator = context.Animator;

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
            _context.DealDamage();
            _context.FinishMeleeAttacking();
        }
    }
}