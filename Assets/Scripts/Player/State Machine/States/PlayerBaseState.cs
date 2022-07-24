using GameFramework.ComponentSystem;
using GameFramework.Loggers;

namespace Player.State_Machine.States
{
    public abstract class PlayerBaseState
    {
        private bool _isRootState;
        private PlayerBaseState _currentSubState;
        private PlayerBaseState _currentSuperState;

        public PlayerBaseState CurrentSubState
        {
            get
            {
                return _currentSubState;
            }
        }
        protected bool IsRootState { set { _isRootState = value; } }
        protected PlayerStateMachine Context { get; }
        protected PlayerStateFactory Factory { get; }
        protected PlayerBase Player { get; }
        protected Core Core { get; }
        protected ILoggerAdapter Logger { get; }

        protected PlayerBaseState(PlayerBase player, PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            Context = currentContext;
            Factory = playerStateFactory;
            Player = player;
            Core = player.Core;
            Logger = player.Logger;
        }

        protected abstract void EnterState();
        protected abstract void UpdateState();
        protected abstract void ExitState();
        protected abstract void CheckSwitchStates();
        protected abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
            _currentSubState?.UpdateState();
        }

        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();

            newState.EnterState();

            if (_isRootState)
            {
                Context.CurrentBaseState = newState;
            }
            else
            {
                _currentSuperState?.SetSubState(newState);
            }
        }

        protected void SetSuperState(PlayerBaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }
    
        protected void SetSubState(PlayerBaseState newSubState)
        {
            _currentSubState?.ExitState();
            _currentSubState = newSubState;
            _currentSubState.EnterState();
            newSubState.SetSuperState(this);
        }
    }
}
