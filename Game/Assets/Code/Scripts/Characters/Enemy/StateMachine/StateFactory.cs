using Characters.Enemy.StateMachine.ConcreteStates;

namespace Characters.Enemy.StateMachine
{
    public class StateFactory
    {
        private readonly Enemy _enemy;
        private readonly StateMachine _stateMachine;

        public StateFactory(Enemy enemy, StateMachine stateMachine)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
        }

        public State MovingToPlayer()
        {
            return new MovingToPlayerState(_enemy, _stateMachine);
        }

        public State AttackPlayer()
        {
            return new AttackPlayerState(_enemy, _stateMachine);
        }

        public State Knockback()
        {
            return new KnockbackState(_enemy, _stateMachine);
        }
    }
}