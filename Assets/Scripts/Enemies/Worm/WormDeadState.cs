using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class WormDeadState : DeadState
    {
        private readonly Worm _worm;

        public WormDeadState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_DeadState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
        }

        public override void Enter()
        {
            base.Enter();

            Vector3 entityPosition = _worm.transform.position;
            Object.Instantiate(StateData._deathBloodParticles, entityPosition,
                StateData._deathBloodParticles.transform.rotation);
            Object.Instantiate(StateData._deathChunkParticles, entityPosition,
                StateData._deathChunkParticles.transform.rotation);
            
            Object.Destroy(_worm.gameObject);
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
