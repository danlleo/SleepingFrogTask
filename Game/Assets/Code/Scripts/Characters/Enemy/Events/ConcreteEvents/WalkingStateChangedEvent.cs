using System;

namespace Characters.Enemy.Events.ConcreteEvents
{
    public class WalkingStateChangedEvent
    {
        public event Action<bool> OnWalkingStateChanged;

        public void Call(bool isWalking)
        {
            OnWalkingStateChanged?.Invoke(isWalking);
        }
    }
}