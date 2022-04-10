using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class WormIdleState : IdleState
    {
        private readonly Worm _worm;
        
        public WormIdleState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData, Worm worm) : base(entity, stateMachine, animatorBoolName, stateData)
        {
            _worm = worm;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsPlayerInMinAggroRange)
            {
                StateMachine.ChangeState(_worm.PlayerDetectedState);
            }
            else if (IsIdleTimeOver)
            {
                StateMachine.ChangeState(_worm.MoveState);
                
            }
        }
    }
}
