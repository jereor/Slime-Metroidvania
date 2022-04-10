using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class LookForPlayerState : State
    {
        protected D_LookForPlayerState StateData;

        protected bool TurnsImmediately;
        protected bool IsPlayerInMinAggroRange;
        protected bool IsAllTurnsDone;
        protected bool IsAllTurnsTimeDone;

        protected float LastTurnTime;

        protected int AmountOfTurnsDone;

        protected LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            IsAllTurnsDone = false;
            IsAllTurnsTimeDone = false;

            LastTurnTime = StartTime;
            AmountOfTurnsDone = 0;
            
            Entity.SetVelocity(0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // TODO: Test that this check is in the right place. Might need to be made a field property
            bool isOverTurnWaitTime = Time.time >= LastTurnTime + StateData._timeBetweenTurns;
            
            if (TurnsImmediately)
            {
                TurnAround();
                TurnsImmediately = false;
            }
            else if (isOverTurnWaitTime 
                     && IsAllTurnsDone == false)
            {
                TurnAround();
            }

            if (AmountOfTurnsDone >= StateData._maxAmountOfTurns)
            {
                IsAllTurnsDone = true;
            }

            if (isOverTurnWaitTime
                && IsAllTurnsDone)
            {
                IsAllTurnsTimeDone = true;
            }
        }

        private void TurnAround()
        {
            Entity.FlipSprite();
            LastTurnTime = Time.time;
            AmountOfTurnsDone++;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleChecks()
        {
            base.HandleChecks();

            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
        }

        public void SetTurnsImmediately(bool turns)
        {
            TurnsImmediately = turns;
        }
    }
}
