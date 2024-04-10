namespace Characters.Enemy.StateMachine.ConcreteStates
{
    public class KnockbackState : State
    {
        private readonly Enemy _enemy;
        private readonly StateMachine _stateMachine;
        
        public KnockbackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
        }

        public override void SubscribeToEvents()
        {
            _enemy.Health.OnReceivedDamage += Health_OnReceivedDamage;
        }

        public override void UnsubscribeFromEvents()
        {
            _enemy.Health.OnReceivedDamage -= Health_OnReceivedDamage;
        }

        public override void OnEnter()
        {
            _enemy.Locomotion.ApplyKnockback(-_enemy.transform.right, 21f, 28f, 0.1f,
                () => { _stateMachine.ChangeState(_enemy.StateFactory.MovingToPlayer()); });
        }
        
        private void Health_OnReceivedDamage()
        {
            _enemy.Locomotion.StopKnockback();
            _enemy.Locomotion.ApplyKnockback(-_enemy.transform.right, 28f, 34f, 0.1f,
                () => { _stateMachine.ChangeState(_enemy.StateFactory.MovingToPlayer()); });
        }
    }
}