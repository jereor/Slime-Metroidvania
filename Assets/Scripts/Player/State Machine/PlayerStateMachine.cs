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

        public void Initialize(PlayerBase player)
        {
            Player = player;
            States = new PlayerStateFactory(player, this);
            CurrentBaseState = States.Grounded();
        }

        // TODO: Split update into logic and physics updates
        public void UpdateStates()
        {
            CurrentBaseState.UpdateStates();
        }
    }
}
