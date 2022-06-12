using Player.State_Machine.States;

namespace Player.State_Machine
{
    public class PlayerStateMachine
    {
        public PlayerBase Player { get; private set; }
        public PlayerStateFactory States { get; private set; }
        public PlayerBaseState CurrentState { get; set; }

        public void Initialize(PlayerBase player)
        {
            Player = player;
            States = new PlayerStateFactory(player, this);
            CurrentState = States.Grounded();
        }

        // TODO: Split update into logic and physics updates
        public void UpdateStates()
        {
            CurrentState.UpdateStates();
        }
    }
}
