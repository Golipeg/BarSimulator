namespace Player
{
    public class PlayerStateMachine
    {
        private IPlayerState _currentState;

        public void ChangeState(IPlayerState newState, Player player)
        {
            if (_currentState != null)
            {
                _currentState.Exit(player);
            }

            _currentState = newState;
            _currentState.Enter(player);
        }

        public void Update(Player player)
        {
            if (_currentState != null)
            {
                _currentState.Update(player);
            }
        }
    }
}