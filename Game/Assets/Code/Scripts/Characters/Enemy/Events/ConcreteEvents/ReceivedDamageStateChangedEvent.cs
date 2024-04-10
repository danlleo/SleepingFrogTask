using System;

namespace Characters.Enemy.Events.ConcreteEvents
{
    public class ReceivedDamageStateChangedEvent
    {
        public event Action<bool> OnReceivedDamage;

        public void Call(bool isDamaged)
        {
            OnReceivedDamage?.Invoke(isDamaged);
        }
    }
}