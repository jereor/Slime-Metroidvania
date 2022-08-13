using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class LookForPlayerState : State
    {
        protected readonly D_LookForPlayerState StateData;

        protected bool CanTurnImmediately;
        protected bool IsPlayerInMinAggroRange;
        protected bool IsDetectingWallBeforePlayer;
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

            bool isOverTurnWaitTime = Time.time >= LastTurnTime + StateData._timeBetweenTurns;
            
            if (CanTurnImmediately)
            {
                TurnAround();
                CanTurnImmediately = false;
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
            IsDetectingWallBeforePlayer = Entity.CheckLongDistanceWall();
        }

        public void SetTurnsImmediately(bool canTurn)
        {
            CanTurnImmediately = canTurn;
        }
    }
}
