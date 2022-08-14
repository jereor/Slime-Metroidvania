using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class DeadState : State
    {
        protected readonly D_DeadState StateData;
        
        protected DeadState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_DeadState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
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
