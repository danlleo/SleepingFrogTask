using System;
using UnityEngine;

namespace Characters.Common
{
    [DisallowMultipleComponent]
    public abstract class Health : MonoBehaviour, IDamagable
    {
        public int CurrentHealthAmount { get; protected set; }

        public abstract int InitialHealth { get; protected set; }
        
        protected virtual void Awake()
        {
            CurrentHealthAmount = InitialHealth;
        }

        public void Damage(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("You're trying to damage someone with negative or null value.");

            CurrentHealthAmount -= amount;
            
            if (CurrentHealthAmount <= 0)
            {
                OnDied();
                return;
            }
            
            OnReceivedDamaged(amount);
        }

        protected abstract void OnReceivedDamaged(int amount);
        protected abstract void OnDied();
    }
}