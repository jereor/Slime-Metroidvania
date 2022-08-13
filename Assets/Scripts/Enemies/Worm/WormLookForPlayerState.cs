using Enemies.States;
using Enemies.States.Data;

namespace Enemies.Worm
{
    public class WormLookForPlayerState : LookForPlayerState
    {
        private readonly Worm _worm;
        
        public WormLookForPlayerState(Worm worm, FiniteStateMachine stateMachine, string animatorBoolName, D_LookForPlayerState stateData) : base(worm, stateMachine, animatorBoolName, stateData)
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

            if (IsPlayerInMinAggroRange && !IsDetectingWallBeforePlayer)
            {
                StateMachine.ChangeState(_worm.PlayerDetectedState);
            }
            else if (IsAllTurnsTimeDone)
            {
                StateMachine.ChangeState(_worm.MoveState);
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
