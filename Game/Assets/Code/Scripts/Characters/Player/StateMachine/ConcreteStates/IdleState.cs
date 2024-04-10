namespace Characters.Player.StateMachine.ConcreteStates
{
    public class IdleState : State
    {
        private readonly Player _player;
        private readonly StateMachine _stateMachine;
        
        public IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
            _player = player;
            _stateMachine = stateMachine;
        }

        public override void SubscribeToEvents()
        {
            _player.Input.OnLeftHooked += Input_OnLeftHooked;
            _player.Input.OnRightHooked += Input_OnRightHooked;
        }

        public override void UnsubscribeFromEvents()
        {
            _player.Input.OnLeftHooked -= Input_OnLeftHooked;
            _player.Input.OnRightHooked -= Input_OnRightHooked;
        }

        public override void OnEnter()
        {
            _player.Events.IdleStateChangedEvent.Call(true);
        }

        public override void OnExit()
        {
            _player.Events.IdleStateChangedEvent.Call(false);
        }

        private void Input_OnLeftHooked()
        {
            _player.PlayerLocomotion.FaceTowards(FaceDirection.West);
            _stateMachine.ChangeState(_player.StateFactory.Attack());
        }
        
        private void Input_OnRightHooked()
        {
            _player.PlayerLocomotion.FaceTowards(FaceDirection.East);
            _stateMachine.ChangeState(_player.StateFactory.Attack());
        }
    }
}