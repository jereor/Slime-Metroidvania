using Player.State_Machine.States;

namespace Player.State_Machine
{
    public class PlayerStateMachine
    {
        public PlayerBase Player { get; private set; }
        public PlayerStateFactory States { get; private set; }
        public PlayerBaseState CurrentBaseState { get; set; }

        public PlayerBaseState CurrentSubState
        {
            get
            {
                return CurrentBaseState.CurrentSubState;
            }
        }

        public void Initialize(PlayerBase player, PlayerStateFactory stateFactory, PlayerBaseState defaultState)
        {
            Player = player;
            States = stateFactory;
            CurrentBaseState = defaultState;
        }

        // TODO: Split update into logic and physics updates
        public void UpdateStates()
        {
            CurrentBaseState.UpdateStates();
        }
    }
}
