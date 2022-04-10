using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class Worm : Entity
    {
        public WormIdleState IdleState { get; private set; }
        public WormMoveState MoveState { get; private set; }
        public WormPlayerDetectedState PlayerDetectedState { get; private set; }
        public ChargeState ChargeState { get; private set; }
        public LookForPlayerState LookForPlayerState { get; private set; }

        [SerializeField] private D_IdleState _idleStateData;
        [SerializeField] private D_MoveState _moveStateData;
        [SerializeField] private D_PlayerDetectedState _playerDetectedStateData;
        [SerializeField] private D_ChargeState _chargeStateData;
        [SerializeField] private D_LookForPlayerState _lookForPlayerStateData;

        public override void Start()
        {
            base.Start();

            // TODO: Make magic string a const
            MoveState = new WormMoveState(this, StateMachine, "move", _moveStateData, this);
            IdleState = new WormIdleState(this, StateMachine, "idle", _idleStateData, this);
            PlayerDetectedState =
                new WormPlayerDetectedState(this, StateMachine, "playerDetected", _playerDetectedStateData, this);
            ChargeState = new WormChargeState(this, StateMachine, "charge", _chargeStateData, this);
            LookForPlayerState =
                new WormLookForPlayerState(this, StateMachine, "lookForPlayer", _lookForPlayerStateData, this);
            
            StateMachine.Initialize(MoveState);
        }
    }
}
