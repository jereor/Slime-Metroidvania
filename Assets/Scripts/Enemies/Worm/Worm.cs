using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class Worm : Entity
    {
        public WormIdleState IdleState { get; private set; }
        public WormMoveState MoveState { get; private set; }
        
        [SerializeField] private D_IdleState _idleStateData;
        [SerializeField] private D_MoveState _moveStateData;

        public override void Start()
        {
            base.Start();

            // TODO: Make magic string a const
            MoveState = new WormMoveState(this, StateMachine, "move", _moveStateData, this);
            IdleState = new WormIdleState(this, StateMachine, "idle", _idleStateData, this);
            StateMachine.Initialize(MoveState);
        }
    }
}
