using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class PlayerDetectedState : State
    {
        protected D_PlayerDetectedState StateData;
        protected bool IsPlayerInMinAggroRange;
        protected bool IsPlayerInMaxAggroRange;
        
        protected PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.SetVelocity(0f);
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
            
            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
            IsPlayerInMaxAggroRange = Entity.CheckPlayerInMaxAggroRange();
        }
    }
}
