using System;

namespace Characters.Player.Events.ConcreteEvents
{
    public class AttackStateChangedEvent
    {
        public event Action<bool> OnAttackStateChanged;

        public void Call(bool isAttacking)
        {
            OnAttackStateChanged?.Invoke(isAttacking);
        }
    }
}