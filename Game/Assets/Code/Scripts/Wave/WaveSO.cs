using UnityEngine;

namespace Wave
{
    [CreateAssetMenu(fileName = "Wave_", menuName = "Scriptable Objects/Waves/Wave")]
    public class WaveSO : ScriptableObject
    {
        [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
        [field: SerializeField] public EnemyWaveEntry[] Enemies { get; private set; }
        [field: SerializeField, Range(0.1f, 10f)] public float SpawnRate { get; private set; }
    }
}
