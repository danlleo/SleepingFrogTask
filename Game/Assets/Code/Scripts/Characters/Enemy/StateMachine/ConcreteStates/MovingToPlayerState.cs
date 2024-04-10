using UnityEngine;

namespace Characters.Enemy.StateMachine.ConcreteStates
{
    public class MovingToPlayerState : State
    {
        private readonly Enemy _enemy;
        private readonly StateMachine _stateMachine;
        private readonly Transform _playerTransform;
        
        public MovingToPlayerState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
            
            if (_enemy.Player != null)
                _playerTransform = _enemy.Player.transform;
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
            _enemy.Locomotion.MoveToPlayer(_playerTransform,
                () => _stateMachine.ChangeState(_enemy.StateFactory.AttackPlayer()));
        }
        
        private void Health_OnReceivedDamage()
        {
            _enemy.Locomotion.StopMovement();
            _stateMachine.ChangeState(_enemy.StateFactory.Knockback());
        }
    }
}