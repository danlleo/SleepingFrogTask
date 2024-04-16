using System;
using Characters.Common;
using Characters.Player;
using Sound;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyHealth : Health
    {
        public static event Action OnAnyEnemyDefeated;
        public event Action OnReceivedDamage;
        public override int InitialHealth { get; protected set; }

        [Header("External references")] 
        [SerializeField] private AudioClip _deathAudioClip;

        private void OnEnable()
        {
            PlayerHealth.OnAnyPlayerDied += PlayerHealth_OnAnyPlayerDied;
        }

        private void OnDisable()
        {
            PlayerHealth.OnAnyPlayerDied -= PlayerHealth_OnAnyPlayerDied;
        }

        public void Initialize(int healthAmount)
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
            OnAnyEnemyDefeated?.Invoke();
            Destroy(gameObject);
        }
        
        private void PlayerHealth_OnAnyPlayerDied()
        {
            Destroy(gameObject);
        }
    }
}