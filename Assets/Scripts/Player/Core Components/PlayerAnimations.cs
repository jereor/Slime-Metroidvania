using GameFramework.ComponentSystem;
using GameFramework.Constants;
using JetBrains.Annotations;
using UnityEngine;

namespace Player.Core_Components
{
    public class PlayerAnimations : Animations
    {
        [SerializeField] private PlayerCombat _playerCombat;
        [SerializeField] private Animator _animator;

        // Animator hashes
        public int IsMovingHash { get; private set; }
        public int IsMeleeAttackingHash { get; private set; }
        public int IsFallingHash { get; private set; }
        public int IsJumpingHash { get; private set; }
        public int IsKnockedBackHash { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            InitializeHashes();
        }

        public void InitializeHashes()
        {
            IsMovingHash = Animator.StringToHash(AnimatorConstants.IS_MOVING);
            IsMeleeAttackingHash = Animator.StringToHash(AnimatorConstants.IS_MELEE_ATTACKING);
            IsFallingHash = Animator.StringToHash(AnimatorConstants.IS_FALLING);
            IsJumpingHash = Animator.StringToHash(AnimatorConstants.IS_JUMPING);
            IsKnockedBackHash = Animator.StringToHash(AnimatorConstants.IS_KNOCKED_BACK);
        }
        
        public void SetAnimatorBool(int animatorHash, bool value)
        {
            _animator.SetBool(animatorHash, value);
        }
        
        // Animation events
        [UsedImplicitly]
        public void FinishAttack()
        {
            _playerCombat.DealDamage();
            _playerCombat.FinishMeleeAttacking();
        }
    }
}
