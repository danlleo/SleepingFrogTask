using System;
using Characters.Common;
using Sound;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerHealth : Health
    {
        public static event Action OnAnyPlayerDied;
        public event Action<int> OnReceivedDamage;
            
        public override int InitialHealth { get; protected set; } = 5;

        [Header("External references")] 
        [SerializeField] private AudioClip _playerHitAudioClip;
        [SerializeField] private AudioClip _deathAudioClip;
        
        protected override void OnReceivedDamaged(int amount)
        {
            SoundFXManager.Instance.PlaySoundFX2DClip(_playerHitAudioClip, .4f);
            OnReceivedDamage?.Invoke(amount);
        }

        protected override void OnDied()
        {
            SoundFXManager.Instance.PlaySoundFX2DClip(_deathAudioClip, .4f);
            OnAnyPlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }
}