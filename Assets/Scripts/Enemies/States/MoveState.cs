using Enemies.States.Data;

namespace Enemies.States
{
    public class MoveState : State
    {
        protected readonly D_MoveState StateData;

        protected bool IsDetectingWall;
        protected bool IsDetectingLedge;
        protected bool IsPlayerInMinAggroRange;
        protected bool IsDetectingWallBeforePlayer;

        public MoveState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData) 
            : base(entity, stateMachine, animatorBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.SetVelocity(StateData._movementSpeed);
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
            
            IsDetectingLedge = Entity.CheckLedge();
            IsDetectingWall = Entity.CheckWall();
            IsDetectingWallBeforePlayer = Entity.CheckLongDistanceWall();
            IsPlayerInMinAggroRange = Entity.CheckPlayerInMinAggroRange();
        }
    }
}
