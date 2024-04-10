using System;
using Characters.Common;
using Sound;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyHealth : Health
    {
        public static event Action OnEnemyDefeated;
        public event Action OnReceivedDamage;
        public override int InitialHealth { get; protected set; }

        [Header("External references")] 
        [SerializeField] private AudioClip _deathAudioClip;
        
        private Enemy _enemy;

        public void InitializeHealth(int healthAmount)
        {
            InitialHealth = healthAmount;
            CurrentHealthAmount = InitialHealth;
        }

        protected override void OnReceivedDamaged(int amount)
        {
            OnReceivedDamage?.Invoke();
        }

        protected override void OnDied()
        {
            SoundFXManager.Instance.PlaySoundFX2DClip(_deathAudioClip, .4f);
            OnEnemyDefeated?.Invoke();
            Destroy(gameObject);
        }
    }
}