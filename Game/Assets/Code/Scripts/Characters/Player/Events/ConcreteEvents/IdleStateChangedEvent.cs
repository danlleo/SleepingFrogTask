using System;

namespace Characters.Player.Events.ConcreteEvents
{
    public class IdleStateChangedEvent
    {
        public event Action<bool> OnIdleStateChanged;

        public void Call(bool isIdling)
        {
            OnIdleStateChanged?.Invoke(isIdling);
        }
    }
}