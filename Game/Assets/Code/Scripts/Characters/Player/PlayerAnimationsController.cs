using DG.Tweening;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(Player))]
    [DisallowMultipleComponent]
    public class PlayerAnimationsController : MonoBehaviour
    {
        private static readonly int s_flashAmount = Shader.PropertyToID("_FlashAmount");
        
        [Header("External references")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("Settings")]
        [SerializeField, Range(0.1f, 1f)] private float _flashDuration;
        
        private Player _player;
        
        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _player.Health.OnReceivedDamage += Player_OnReceivedDamage;
            _player.Events.AttackStateChangedEvent.OnAttackStateChanged += Player_OnAttackStateChanged;
            _player.Events.IdleStateChangedEvent.OnIdleStateChanged += Player_OnIdleStateChanged;
        }

        private void OnDisable()
        {
            _player.Health.OnReceivedDamage -= Player_OnReceivedDamage;
            _player.Events.AttackStateChangedEvent.OnAttackStateChanged -= Player_OnAttackStateChanged;
            _player.Events.IdleStateChangedEvent.OnIdleStateChanged -= Player_OnIdleStateChanged;
        }

        private void Player_OnReceivedDamage(int amount)
        {
            _spriteRenderer.material.DOKill();
            _spriteRenderer.material.DOFloat(1f, s_flashAmount, _flashDuration / 2)
                .OnComplete(() =>
            {
                _spriteRenderer.material.DOFloat(0f, s_flashAmount, _flashDuration / 2);
            });
        }

        private void Player_OnAttackStateChanged(bool isAttacking)
        {
            _animator.SetBool(PlayerAnimationParams.IsAttacking, isAttacking);
        }
        
        private void Player_OnIdleStateChanged(bool isIdling)
        {
            _animator.SetBool(PlayerAnimationParams.IsIdling, isIdling);
        }
    }
}
