using UnityEngine;
using Enemies.States.Data;

namespace Enemies.States
{
    public class IdleState : State
    {
        public bool CanFlipAfterIdle { get; set; }
        
        protected readonly D_IdleState StateData;
        
        protected bool IsIdleTimeOver;
        protected bool IsPlayerInMinAggroRange;

        protected float IdleTime;
        
        protected IdleState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData) : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.SetVelocity(0f);
            IsIdleTimeOver = false;
            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();

            if (CanFlipAfterIdle)
            {
                Entity.FlipSprite();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + IdleTime)
            {
                IsIdleTimeOver = true;
            }
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

        public void SetFlipAfterIdle(bool flipOrNot)
        {
            CanFlipAfterIdle = flipOrNot;
        }

        private void SetRandomIdleTime()
        {
            IdleTime = Random.Range(
                StateData._minIdleTime, 
                StateData._maxIdleTime);
        }
    }
}
