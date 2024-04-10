using Characters.Common;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Enemy))]
    [DisallowMultipleComponent]
    public class EnemyMelee : MonoBehaviour
    {
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        public void Attack()
        {
            if (_enemy.Player.TryGetComponent(out IDamagable damagable))
            {
                damagable.Damage(_enemy.AttackDamage);
            }
        }
    }
}