using Characters.Enemy.Events;
using Characters.Enemy.StateMachine;
using Characters.Player;
using UnityEngine;
using Zenject;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyMelee))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyLocomotion))]
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        public Player.Player Player { get; private set; }
        public StateFactory StateFactory { get; private set; }
        public EnemyHealth Health { get; private set; }
        public EnemyLocomotion Locomotion { get; private set; }
        public EnemyMelee EnemyMelee { get; private set; }
        public EnemyEvents Events { get; private set; }
        
        public float MovingSpeed { get; private set; }
        public int AttackDamage { get; private set; }
        public int HealthAmount { get; private set; }
        
        [Header("External references")]
        [SerializeField] private SpriteRenderer _spriteRenderer; 
        
        private StateMachine.StateMachine _stateMachine;

        [Inject]
        private void Construct(Player.Player player)
        {
            Player = player;
        }
        
        private void Awake()
        {
            Health = GetComponent<EnemyHealth>();
            Locomotion = GetComponent<EnemyLocomotion>();
            EnemyMelee = GetComponent<EnemyMelee>();
            
            Events = new EnemyEvents();
            _stateMachine = new StateMachine.StateMachine();
            StateFactory = new StateFactory(this, _stateMachine);
        }
        
        private void OnEnable()
        {
            _stateMachine.CurrentState?.SubscribeToEvents();
            PlayerHealth.OnAnyPlayerDied += PlayerHealth_OnAnyPlayerDied;
        }

        private void OnDisable()
        {
            _stateMachine.CurrentState.UnsubscribeFromEvents();
            PlayerHealth.OnAnyPlayerDied -= PlayerHealth_OnAnyPlayerDied;
        }

        private void Start()
        {
            _stateMachine.Initialize(StateFactory.MovingToPlayer());
            Health.InitializeHealth(HealthAmount);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Tick();
        }

        private void OnDestroy()
        {
            _stateMachine.CurrentState.OnExit();
        }
        
        private void PlayerHealth_OnAnyPlayerDied()
        {
            Destroy(gameObject);
        }

        public void Initialize(float movingSpeed, int attackDamage, int healthAmount, Color color)
        {
            MovingSpeed = movingSpeed;
            AttackDamage = attackDamage;
            HealthAmount = healthAmount;
            _spriteRenderer.color = color;
        }
        
        public class Factory : PlaceholderFactory<Enemy> { }
    }
}
