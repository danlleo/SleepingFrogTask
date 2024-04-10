using Characters.Player.Events.ConcreteEvents;

namespace Characters.Player.Events
{
    public class PlayerEvents
    {
        public readonly AttackStateChangedEvent AttackStateChangedEvent = new();
        public readonly IdleStateChangedEvent IdleStateChangedEvent = new();
    }
}