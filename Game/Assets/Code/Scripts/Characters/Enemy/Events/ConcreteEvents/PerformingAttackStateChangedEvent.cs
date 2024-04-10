using System;

namespace Characters.Enemy.Events.ConcreteEvents
{
    public class PerformingAttackStateChangedEvent
    {
        public event Action<bool> OnPerformingAttackStateChanged;

        public void Call(bool isAttacking)
        {
            OnPerformingAttackStateChanged?.Invoke(isAttacking);
        }
    }
}