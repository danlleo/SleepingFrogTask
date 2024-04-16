using UnityEngine;

namespace Wave
{
    [CreateAssetMenu(fileName = "Wave_", menuName = "Scriptable Objects/Waves/Wave")]
    public class WaveSO : ScriptableObject
    {
        [field: SerializeField, Tooltip("Fill with the array of points where you want to spawn enemies.")]
        public Transform[] SpawnPoints { get; private set; }

        [field: SerializeField, Tooltip("Fill with enemy variety. Order is crucial. In this order enemies will spawn.")]
        public EnemyWaveEntry[] Enemies { get; private set; }

        [field: SerializeField, Range(0.1f, 10f), Tooltip("Time before spawning new enemy.")]
        public float SpawnRate { get; private set; }
    }
}
