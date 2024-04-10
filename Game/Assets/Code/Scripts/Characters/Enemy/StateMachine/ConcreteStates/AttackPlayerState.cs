using System.Collections;
using Misc;
using UnityEngine;

namespace Characters.Enemy.StateMachine.ConcreteStates
{
    public class AttackPlayerState : State
    {
        private const float DelayTimeBeforeNextAttack = 1f;
        
        private readonly Enemy _enemy;
        private readonly StateMachine _stateMachine;

        private Coroutine _persistentAttackRoutine;
        
        public AttackPlayerState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
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
            _enemy.Events.PerformingAttackStateChangedEvent.Call(true);
            KeepAttacking();
        }

        public override void OnExit()
        {
            _enemy.Events.PerformingAttackStateChangedEvent.Call(false);
        }

        private void KeepAttacking()
        {
            CoroutineHandler.StartAndAssignIfNull(_enemy, ref _persistentAttackRoutine, PersistentAttackRoutine());
        }

        private IEnumerator PersistentAttackRoutine()
        {
            while (true)
            {
                _enemy.EnemyMelee.Attack();
                yield return new WaitForSeconds(DelayTimeBeforeNextAttack);
            }
        }
        
        private void Health_OnReceivedDamage()
        {
            CoroutineHandler.ClearAndStopCoroutine(_enemy, ref _persistentAttackRoutine);
            _stateMachine.ChangeState(_enemy.StateFactory.Knockback());
        }
    }
}