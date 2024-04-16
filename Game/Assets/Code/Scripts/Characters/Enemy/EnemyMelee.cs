using Characters.Common;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Enemy))]
    [DisallowMultipleComponent]
    public class EnemyMelee : MonoBehaviour
    {
        private Enemy _enemy;

        private int _attackDamage;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        public void Initialize(int attackDamage)
        {
            _attackDamage = attackDamage;
        }
        
        public void Attack()
        {
            if (_enemy.Player == null) return;
            
            if (_enemy.Player.TryGetComponent(out IDamagable damagable))
            {
                damagable.Damage(_attackDamage);
            }
        }
    }
}