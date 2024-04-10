using Characters.Common;
using Sound;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(Player))]
    [DisallowMultipleComponent]
    public class PlayerMelee : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private AudioClip _hitAudioClip;
        
        [Header("Settings")]
        [SerializeField, Range(0.1f, 5f)] private float _attackDistance;
        [SerializeField, Range(1, 3)] private int _attackDamage = 1;
        [SerializeField] private LayerMask _enemyLayers;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        public void Attack()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackDistance, _enemyLayers);
            
            foreach (Collider2D enemy in hitEnemies)
            {
                if (!enemy.TryGetComponent(out IDamagable damagable)) continue;

                SoundFXManager.Instance.PlaySoundFX2DClip(_hitAudioClip, .7f);
                _player.Events.AttackStateChangedEvent.Call(true);
                damagable.Damage(_attackDamage);
            }
        }
    }
}
