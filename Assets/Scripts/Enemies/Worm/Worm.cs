using Enemies.States;
using Enemies.States.Data;
using UnityEngine;
using Utility;

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

        [SerializeField] private D_IdleState _idleStateData;
        [SerializeField] private D_MoveState _moveStateData;
        [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
        [SerializeField] private D_ChargeState _chargeStateData;
        [SerializeField] private D_LookForPlayerState _lookForPlayerStateData;
        [SerializeField] private D_MeleeAttackState _meleeAttackStateData;

        [SerializeField] private Transform _meleeAttackPosition;

        public override void Start()
        {
            base.Start();

            IdleState = new WormIdleState(this, StateMachine, AnimatorConstants.IS_IDLE, _idleStateData, this);
            
            MoveState = new WormMoveState(this, StateMachine, AnimatorConstants.IS_MOVING, _moveStateData, this);
            
            PlayerDetectedState =
                new WormPlayerDetectedState(this, StateMachine, AnimatorConstants.IS_PLAYER_DETECTED,
                    _playerDetectedStateData, this);
            
            ChargeState =
                new WormChargeState(this, StateMachine, AnimatorConstants.IS_CHARGING, _chargeStateData, this);
            
            LookForPlayerState =
                new WormLookForPlayerState(this, StateMachine, AnimatorConstants.IS_LOOKING_FOR_PLAYER,
                    _lookForPlayerStateData, this);
            
            MeleeAttackState = new WormMeleeAttackState(this, StateMachine, AnimatorConstants.IS_MELEE_ATTACKING,
                _meleeAttackPosition,
                _meleeAttackStateData, this);
            
            StateMachine.Initialize(MoveState);
        }
    }
}
