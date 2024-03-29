using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class WormMeleeAttackState : MeleeAttackState
    {
        private readonly Worm _worm;
        
        public WormMeleeAttackState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(worm, stateMachine, animatorBoolName, attackPosition, stateData)
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

            if (!IsAnimationFinished)
            {
                return;
            }
            
            if (IsPlayerInMinAggroRange)
            {
                StateMachine.ChangeState(_worm.ChargeState);
            }
            else
            {
                StateMachine.ChangeState(_worm.LookForPlayerState);
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

        public override void TriggerAttack()
        {
            base.TriggerAttack();
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
