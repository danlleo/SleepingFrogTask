using Characters.Enemy.Events.ConcreteEvents;

namespace Characters.Enemy.Events
{
    public class EnemyEvents
    {
        public readonly WalkingStateChangedEvent WalkingStateChangedEvent = new();
        public readonly ReceivedDamageStateChangedEvent ReceivedDamageStateChangedEvent = new();
        public readonly PerformingAttackStateChangedEvent PerformingAttackStateChangedEvent = new();
    }
}