using Characters.Player.Events;
using Characters.Player.StateMachine;
using Input;
using UnityEngine;
using Zenject;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerMelee))]
    [RequireComponent(typeof(PlayerLocomotion))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public IInput Input { get; private set; }
        public StateFactory StateFactory { get; private set; }
        public PlayerLocomotion PlayerLocomotion { get; private set; }
        public PlayerMelee PlayerMelee { get; private set; }
        public PlayerHealth Health { get; private set; }
        public PlayerEvents Events;
        
        private StateMachine.StateMachine _stateMachine;

        [Inject]
        private void Construct(IInput input)
        {
            Input = input;
        }
        
        private void Awake()
        {
            PlayerLocomotion = GetComponent<PlayerLocomotion>();
            PlayerMelee = GetComponent<PlayerMelee>();
            Health = GetComponent<PlayerHealth>();
            
            Events = new PlayerEvents();
            _stateMachine = new StateMachine.StateMachine();
            StateFactory = new StateFactory(this, _stateMachine);
        }
        
        private void OnEnable()
        {
            _stateMachine.CurrentState?.SubscribeToEvents();
        }

        private void OnDisable()
        {
            _stateMachine.CurrentState.UnsubscribeFromEvents();
        }

        private void Start()
        {
            _stateMachine.Initialize(StateFactory.Idle());
        }

        private void Update()
        {
            _stateMachine.CurrentState.Tick();
        }

        private void OnDestroy()
        {
            _stateMachine.CurrentState.OnExit();
        }
    }
}
