using Player.State_Machine.States;

namespace Player.State_Machine
{
    public class PlayerStateMachine
    {
        public Core_Components.Player Player { get; private set; }
        public PlayerStateFactory States { get; private set; }
        public PlayerBaseState CurrentState { get; set; }

        public void Initialize(Core_Components.Player player)
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
