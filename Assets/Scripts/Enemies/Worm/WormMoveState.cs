using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormMoveState : MoveState
    {
        private readonly Worm _worm; 
            
        public WormMoveState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
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
            
            if (IsDetectingLedge == false 
                     || IsDetectingWall)
            {
                _worm.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_worm.IdleState);
            }
            else if (IsPlayerInMinAggroRange && !IsDetectingWallBeforePlayer)
            {
                StateMachine.ChangeState(_worm.PlayerDetectedState);
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
    }
}
