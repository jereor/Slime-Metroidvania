namespace Player.State_Machine
{
    public abstract class PlayerBaseState
    {
        private bool _isRootState;
        private PlayerBaseState _currentSubState;
        private PlayerBaseState _currentSuperState;

        protected bool IsRootState { set { _isRootState = value; } }
        protected PlayerStateMachine Context { get; }
        protected PlayerStateFactory Factory { get; }

        protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            Context = currentContext;
            Factory = playerStateFactory;
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
                Context.CurrentState = newState;
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
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}
