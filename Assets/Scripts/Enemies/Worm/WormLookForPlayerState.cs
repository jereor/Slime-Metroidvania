using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class WormLookForPlayerState : LookForPlayerState
    {
        protected WormLookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animatorBoolName, stateData)
        {
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
