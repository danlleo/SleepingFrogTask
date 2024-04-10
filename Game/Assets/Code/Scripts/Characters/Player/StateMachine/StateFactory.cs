using Characters.Player.StateMachine.ConcreteStates;

namespace Characters.Player.StateMachine
{
    public class StateFactory
    {
        private readonly Player _player;
        private readonly StateMachine _stateMachine;

        public StateFactory(Player player, StateMachine stateMachine)
        {
            _player = player;
            _stateMachine = stateMachine;
        }

        public State Idle()
        {
            return new IdleState(_player, _stateMachine);
        }

        public State Attack()
        {
            return new AttackState(_player, _stateMachine);
        }
    }
}