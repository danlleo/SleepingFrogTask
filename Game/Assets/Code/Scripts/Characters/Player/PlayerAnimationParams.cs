using UnityEngine;

namespace Characters.Player
{
    public static class PlayerAnimationParams
    {
        public static int IsIdling = Animator.StringToHash(nameof(IsIdling));
        public static int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    }
}