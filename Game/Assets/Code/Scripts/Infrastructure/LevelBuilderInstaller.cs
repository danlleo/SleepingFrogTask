using Characters.Enemy;
using Characters.Player;
using UnityEngine;
using Wave;
using Zenject;

namespace Infrastructure
{
    public class LevelBuilderInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private WaveSO[] _waveSOArray;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindEnemyFactory();
            BindWaveManager();
        }

        private void BindPlayer()
        {
            Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position,
                Quaternion.identity, null);

            Container
                .BindInstance(player)
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyFactory()
        {
            Container
                .BindFactory<Enemy, Enemy.Factory>()
                .FromComponentInNewPrefab(_enemyPrefab);
        }
        
        private void BindWaveManager()
        {
            Container
                .BindInterfacesAndSelfTo<WaveManager>()
                .AsSingle()
                .WithArguments(_waveSOArray)
                .NonLazy();
        }
    }
}