using UnityEngine;
using DG.Tweening;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Enemy))]
    [DisallowMultipleComponent]
    public class EnemyAnimationsController : MonoBehaviour
    {
        private static readonly int s_flashAmount = Shader.PropertyToID("_FlashAmount");
        
        [Header("External references")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Header("Settings")]
        [SerializeField] private float _squishDuration = 0.1f;
        [SerializeField, Range(1.1f, 2f)] private float _squishScale = 1.2f;
        
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            _enemy.Events.ReceivedDamageStateChangedEvent.OnReceivedDamage += EnemyOnReceivedDamageStateChanged;
            _enemy.Events.WalkingStateChangedEvent.OnWalkingStateChanged += Enemy_OnWalkingStateChanged;
            _enemy.Events.PerformingAttackStateChangedEvent.OnPerformingAttackStateChanged +=
                Enemy_OnPerformingAttackStateChanged;
        }

        private void OnDisable()
        {
            _enemy.Events.ReceivedDamageStateChangedEvent.OnReceivedDamage -= EnemyOnReceivedDamageStateChanged;
            _enemy.Events.WalkingStateChangedEvent.OnWalkingStateChanged -= Enemy_OnWalkingStateChanged;
            _enemy.Events.PerformingAttackStateChangedEvent.OnPerformingAttackStateChanged -=
                Enemy_OnPerformingAttackStateChanged;
        }

        private void PlaySquishAnimation()
        {
            Vector3 initialScale = transform.localScale;
            Vector3 endScale = new(transform.localScale.x, _squishScale, transform.localScale.z);
            
            Material material = _spriteRenderer.material; 

            Sequence squishAnimationSequence = DOTween.Sequence();

            squishAnimationSequence.Append(transform.DOScale(endScale, _squishDuration / 2)
                .SetEase(Ease.OutQuad));

            squishAnimationSequence.Join(DOVirtual.Float(0, 1, _squishDuration / 2, value =>
            {
                material.SetFloat(s_flashAmount, value);
            }).SetEase(Ease.OutQuad));

            squishAnimationSequence.Append(DOVirtual.Float(1, 0, _squishDuration / 2, value =>
            {
                material.SetFloat(s_flashAmount, value);
            }).SetEase(Ease.OutQuad));
            squishAnimationSequence.Join(transform.DOScale(initialScale, _squishDuration));
        }
        
        private void Enemy_OnWalkingStateChanged(bool isWalking)
        {
            _animator.SetBool(EnemyAnimationParams.IsWalking, isWalking);
        }

        private void EnemyOnReceivedDamageStateChanged(bool isDamaged)
        {
            _animator.SetBool(EnemyAnimationParams.IsKnockingback, isDamaged);
            PlaySquishAnimation();
        }
        
        private void Enemy_OnPerformingAttackStateChanged(bool isAttacking)
        {
            _animator.SetBool(EnemyAnimationParams.IsAttacking, isAttacking);
        }
    }
}