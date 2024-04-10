using UnityEngine;

namespace Characters.Enemy
{
    [CreateAssetMenu(fileName = "Enemy_", menuName = "Scriptable Objects/Enemies/Enemy")]
    public class EnemySO : ScriptableObject
    {
        [field: SerializeField, Range(1f, 10f)] public float MovingSpeed { get; private set; }
        [field: SerializeField, Range(1, 3)] public int AttackDamage { get; private set; }
        [field: SerializeField, Range(1, 3)] public int HealthAmount { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}