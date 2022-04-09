using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class MoveState : State
    {
        protected D_MoveState StateData;

        protected bool IsDetectingWall;
        protected bool IsDetectingLedge;
        
        public MoveState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData) 
            : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.SetVelocity(StateData._movementSpeed);
            IsDetectingLedge = Entity.CheckLedge();
            IsDetectingWall = Entity.CheckWall();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            IsDetectingLedge = Entity.CheckLedge();
            IsDetectingWall = Entity.CheckWall();
        }
    }
}
