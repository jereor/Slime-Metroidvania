using Enemies.States;
using Enemies.States.Data;
using GameFramework;
using GameFramework.Constants;
using UnityEngine;

namespace Enemies.Worm
{
    public class Worm : Entity
    {
        public WormIdleState IdleState { get; private set; }
        public WormMoveState MoveState { get; private set; }
        public WormPlayerDetectedState PlayerDetectedState { get; private set; }
        public WormChargeState ChargeState { get; private set; }
        public WormLookForPlayerState LookForPlayerState { get; private set; }
        public WormMeleeAttackState MeleeAttackState { get; private set; }
        public WormDamageState DamageState { get; private set; }
        public WormStunState StunState { get; private set; }
        public WormDeadState DeadState { get; private set; }

        [SerializeField] private D_IdleState _idleStateData;
        [SerializeField] private D_MoveState _moveStateData;
        [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
        [SerializeField] private D_ChargeState _chargeStateData;
        [SerializeField] private D_LookForPlayerState _lookForPlayerStateData;
        [SerializeField] private D_MeleeAttackState _meleeAttackStateData;
        [SerializeField] private D_StunState _stunStateData;
        [SerializeField] private D_DeadState _deadStateData;

        [SerializeField] private Transform _meleeAttackPosition;

        public override void Start()
        {
            base.Start();

            IdleState =
                new WormIdleState(this, StateMachine, AnimatorConstants.IS_IDLE,
                    _idleStateData);

            MoveState =
                new WormMoveState(this, StateMachine, AnimatorConstants.IS_MOVING,
                    _moveStateData);

            PlayerDetectedState =
                new WormPlayerDetectedState(this, StateMachine, AnimatorConstants.IS_PLAYER_DETECTED,
                    _playerDetectedStateData);

            ChargeState =
                new WormChargeState(this, StateMachine, AnimatorConstants.IS_CHARGING,
                    _chargeStateData);

            LookForPlayerState =
                new WormLookForPlayerState(this, StateMachine, AnimatorConstants.IS_LOOKING_FOR_PLAYER,
                    _lookForPlayerStateData);

            MeleeAttackState =
                new WormMeleeAttackState(this, StateMachine, AnimatorConstants.IS_MELEE_ATTACKING,
                    _meleeAttackPosition, _meleeAttackStateData);

            DamageState = new WormDamageState(this, StateMachine, AnimatorConstants.IS_DAMAGED);
            
            StunState = 
                new WormStunState(this, StateMachine, AnimatorConstants.IS_STUNNED, 
                    _stunStateData);

            DeadState = new WormDeadState(this, StateMachine, AnimatorConstants.IS_DEAD, _deadStateData);

            StateMachine.Initialize(MoveState);
        }

        public override void Damage(AttackDetails attackDetails)
        {
            base.Damage(attackDetails);
            
            StateMachine.ChangeState(DamageState);
            
            if (IsDead)
            {
                StateMachine.ChangeState(DeadState);
            }
            else if (StateMachine.CurrentState != StunState
                && IsStunned)
            {
                StateMachine.ChangeState(StunState);
            }
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData._attackRadius);
        }
    }
}
