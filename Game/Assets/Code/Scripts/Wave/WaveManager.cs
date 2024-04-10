using System;
using System.Collections;
using Characters.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Wave
{
    public class WaveManager : IDisposable
    {
        public static event Action OnAnyWaveSpawned;
        public static event Action OnAnyWaveCleared;
        public static event Action OnAnyClearedAllWaves;
        
        public int EnemiesCount { get; private set; }
        public int DefeatedEnemiesCount { get; private set; }
        
        private readonly WaveSO[] _waves;
        private readonly Enemy.Factory _enemyFactory;
        
        private WaveSO _currentWave;

        private int _currentWaveIndex;
        
        public WaveManager(Enemy.Factory enemyFactory, WaveSO[] waves)
        {
            _enemyFactory = enemyFactory;
            _waves = waves;
            _currentWave = _waves[_currentWaveIndex];
            
            EnemyHealth.OnEnemyDefeated += EnemyHealth_OnAnyEnemyDefeated;
        }

        public void Dispose()
        {
            EnemyHealth.OnEnemyDefeated -= EnemyHealth_OnAnyEnemyDefeated;
        }
         
        public void SpawnWave(MonoBehaviour owner)
        {
            CountAllEnemiesInAWave();
            owner.StartCoroutine(SpawnWaveRoutine());
        }
        
        private void CountAllEnemiesInAWave()
        {
            EnemiesCount = 0;
            
            foreach (EnemyWaveEntry enemyWaveEntry in _currentWave.Enemies)
            {
                EnemiesCount += enemyWaveEntry.Count;
            }
        }
        
        private IEnumerator SpawnWaveRoutine()
        {
            OnAnyWaveSpawned?.Invoke();
            
            foreach (EnemyWaveEntry enemyWaveEntry in _currentWave.Enemies)
            {
                for (int i = 0; i < enemyWaveEntry.Count; i++)
                {
                    Enemy enemy = _enemyFactory.Create();
                    enemy.transform.position =
                        _currentWave.SpawnPoints[Random.Range(0, _currentWave.SpawnPoints.Length)].position;
                    enemy.Initialize(enemyWaveEntry.EnemyType.MovingSpeed, enemyWaveEntry.EnemyType.AttackDamage,
                        enemyWaveEntry.EnemyType.HealthAmount,
                        enemyWaveEntry.EnemyType.Color);
                    
                    yield return new WaitForSeconds(_currentWave.SpawnRate);
                }
            }
        }
        
        private void EnemyHealth_OnAnyEnemyDefeated()
        {
            DefeatedEnemiesCount++;

            if (DefeatedEnemiesCount != EnemiesCount) return;
            _currentWaveIndex++;

            if (_currentWaveIndex < _waves.Length)
            {
                _currentWave = _waves[_currentWaveIndex];
                DefeatedEnemiesCount = 0;
                OnAnyWaveCleared?.Invoke();
                return;
            }
                
            OnAnyClearedAllWaves?.Invoke();
        }
    }
}
