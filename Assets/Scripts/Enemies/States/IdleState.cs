using UnityEngine;
using Enemies.States.Data;

namespace Enemies.States
{
    public class IdleState : State
    {
        public bool FlipsAfterIdle { get; set; }
        
        protected D_IdleState StateData;
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
            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();

            if (FlipsAfterIdle)
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
            
            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
        }

        public void SetFlipAfterIdle(bool flipOrNot)
        {
            FlipsAfterIdle = flipOrNot;
        }

        private void SetRandomIdleTime()
        {
            IdleTime = Random.Range(StateData.minIdleTime, StateData.maxIdleTime);
        }
    }
}
