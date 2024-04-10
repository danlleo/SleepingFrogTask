using Characters.Enemy;
using UnityEngine;

namespace Wave
{
    [System.Serializable]
    public struct EnemyWaveEntry
    {
        [field: SerializeField] public EnemySO EnemyType { get; private set; }
        [field: SerializeField, Range(1, 10)] public int Count { get; private set; }
    }
}