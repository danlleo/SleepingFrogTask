using System.Collections;
using Misc;
using UnityEngine;

namespace Characters.Player.StateMachine.ConcreteStates
{
    public class AttackState : State
    {
        private const float AttackTimeInSeconds = .2f;
        
        private readonly Player _player;
        private readonly StateMachine _stateMachine;

        private Coroutine _attackRoutine;
        
        public AttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
            _player = player;
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            Attack();
        }

        private void Attack()
        {
            CoroutineHandler.StartAndAssignIfNull(_player, ref _attackRoutine, AttackRoutine());
        }

        private IEnumerator AttackRoutine()
        {
            _player.PlayerMelee.Attack();
            yield return new WaitForSeconds(AttackTimeInSeconds);
            _player.Events.AttackStateChangedEvent.Call(false);
            _stateMachine.ChangeState(_player.StateFactory.Idle());
        }
    }
}