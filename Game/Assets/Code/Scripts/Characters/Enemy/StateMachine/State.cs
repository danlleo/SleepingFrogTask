namespace Characters.Enemy.StateMachine
{
    public class State
    {
        protected readonly Enemy Enemy;
        protected readonly StateMachine StateMachine;

        public State(Enemy enemy, StateMachine stateMachine)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
        }
        
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void SubscribeToEvents() { }
        public virtual void UnsubscribeFromEvents() { }
        public virtual void Tick() { }
    }
}