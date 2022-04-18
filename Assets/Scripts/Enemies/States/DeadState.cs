using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class DeadState : State
    {
        protected D_DeadState StateData;
        
        protected DeadState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_DeadState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            Vector3 entityPosition = Entity.transform.position;
            Object.Instantiate(StateData._deathBloodParticles, entityPosition,
                StateData._deathBloodParticles.transform.rotation);
            Object.Instantiate(StateData._deathChunkParticles, entityPosition,
                StateData._deathChunkParticles.transform.rotation);
            
            Entity.gameObject.SetActive(false);
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
