using System;
using System.Collections;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Enemy))]
    [DisallowMultipleComponent]
    public class EnemyLocomotion : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Range(1f, 7f)] private float _stoppingDistance;

        private Enemy _enemy;
        
        private Coroutine _moveToPlayerRoutine;
        private Coroutine _knockbackRoutine;

        private float _movingSpeed;
        
        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        public void Initialize(float movingSpeed)
        {
            _movingSpeed = movingSpeed;
        }
        
        public void MoveToPlayer(Transform playerTransform, Action onReached)
        {
            if (playerTransform == null) return;
            
            CoroutineHandler.StartAndAssignIfNull(this, ref _moveToPlayerRoutine,
                MoveToPlayRoutine(playerTransform, onReached));
        }

        public void StopMovement()
        {
            CoroutineHandler.ClearAndStopCoroutine(this, ref _moveToPlayerRoutine);
            _enemy.Events.WalkingStateChangedEvent.Call(false);
        }

        public void ApplyKnockback(Vector2 sourcePosition, float minimalStrength, float maximumStrength, float duration,
            Action onReached)
        {
            Vector2 knockbackDirection =
                (transform.position - new Vector3(sourcePosition.x, sourcePosition.y, transform.position.z)).normalized;

            CoroutineHandler.StartAndAssignIfNull(this, ref _knockbackRoutine,
                KnockbackRoutine(knockbackDirection, minimalStrength, maximumStrength, duration, onReached));
        }

        public void StopKnockback()
        {
            CoroutineHandler.ClearAndStopCoroutine(this, ref _knockbackRoutine);
        }
        
        private void FacePlayer(Transform playerTransform)
        {
            if (playerTransform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z);
            }
            else if (playerTransform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                    transform.localScale.y, transform.localScale.z);
            }
        }
        
        private IEnumerator MoveToPlayRoutine(Transform playerTransform, Action onReached)
        {
            _enemy.Events.WalkingStateChangedEvent.Call(true);

            Vector2 targetPosition = playerTransform.position;
            
            FacePlayer(playerTransform);
            
            while (Vector2.Distance(transform.position, targetPosition) > _stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(targetPosition.x, transform.position.y, transform.position.z),
                    _movingSpeed * Time.deltaTime);

                yield return null;
            }
            
            _enemy.Events.WalkingStateChangedEvent.Call(false);
            _moveToPlayerRoutine = null;
            onReached?.Invoke();
        }

        private IEnumerator KnockbackRoutine(Vector2 direction, float minimalStrength, float maximumStrength,
            float duration, Action onReached)
        {
            if (minimalStrength > maximumStrength)
                throw new ArgumentException("Minimal strength can't be higher than maximum strength");

            _enemy.Events.ReceivedDamageStateChangedEvent.Call(true);
            
            float endTime = Time.time + duration;
            float randomStrength = Random.Range(minimalStrength, maximumStrength);

            while (Time.time < endTime)
            {
                transform.position += new Vector3(direction.x, 0f, 0) * randomStrength * Time.deltaTime;

                float decreaseFactor = randomStrength / (duration * 60);
                randomStrength -= decreaseFactor * Time.deltaTime;

                yield return null;
            }

            _enemy.Events.ReceivedDamageStateChangedEvent.Call(false);
            _knockbackRoutine = null;
            onReached?.Invoke();
        }
    }
}