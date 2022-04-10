using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class WormLookForPlayerState : LookForPlayerState
    {
        private readonly Worm _worm;
        
        public WormLookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_LookForPlayerState stateData, Worm worm) : base(entity, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsPlayerInMinAggroRange)
            {
                StateMachine.ChangeState(_worm.PlayerDetectedState);
            }
            else if (IsAllTurnsTimeDone)
            {
                StateMachine.ChangeState(_worm.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleChecks()
        {
            base.HandleChecks();
        }
    }
}
