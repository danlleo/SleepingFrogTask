using UnityEngine;

namespace Characters.Enemy
{
    public static class EnemyAnimationParams
    {
        public static int IsWalking = Animator.StringToHash(nameof(IsWalking));
        public static int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
        public static int IsKnockingback = Animator.StringToHash(nameof(IsKnockingback));
    }
}