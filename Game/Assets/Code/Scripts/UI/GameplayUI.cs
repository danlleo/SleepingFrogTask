using System.Collections.Generic;
using Characters.Enemy;
using Characters.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wave;
using Zenject;

namespace UI
{
    [DisallowMultipleComponent]
    public class GameplayUI : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private HorizontalLayoutGroup _horizontalLayout;
        [SerializeField] private Image _heartImagePrefab;
        [SerializeField] private TextMeshProUGUI _enemiesCountText;

        private Player _player;
        private WaveManager _waveManager;
        private readonly List<Image> _heartImagesList = new();
        
        [Inject]
        private void Construct(Player player, WaveManager waveManager)
        {
            _player = player;
            _waveManager = waveManager;
        }

        private void OnEnable()
        {
            _player.Health.OnReceivedDamage += Player_OnReceivedDamage;
            EnemyHealth.OnEnemyDefeated += EnemyHealth_OnAnyEnemyDefeated;
            WaveManager.OnAnyWaveSpawned += WaveManager_OnAnyWaveSpawned;
        }

        private void OnDisable()
        {
            _player.Health.OnReceivedDamage -= Player_OnReceivedDamage;
            EnemyHealth.OnEnemyDefeated -= EnemyHealth_OnAnyEnemyDefeated;
            WaveManager.OnAnyWaveSpawned -= WaveManager_OnAnyWaveSpawned;
        }

        private void Start()
        {
            SpawnHearts();
        }

        private void UpdateDefeatedEnemiesCountText()
        {
            _enemiesCountText.text = $"{_waveManager.DefeatedEnemiesCount} / {_waveManager.EnemiesCount}";
        }
        
        private void SpawnHearts()
        {
            for (int i = 0; i < _player.Health.CurrentHealthAmount; i++)
            {
                Image heartImage = Instantiate(_heartImagePrefab, _horizontalLayout.transform);
                _heartImagesList.Add(heartImage);
            }
        }

        private void RemoveHeartsInQuantity(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (_heartImagesList.Count > 0)
                {
                    int lastIndex = _heartImagesList.Count - 1;
                    Destroy(_heartImagesList[lastIndex].gameObject);
                    _heartImagesList.RemoveAt(lastIndex);
                }
                else
                {
                    break;
                }
            }
        }
        
        private void EnemyHealth_OnAnyEnemyDefeated()
        {
            UpdateDefeatedEnemiesCountText();
        }
        
        private void Player_OnReceivedDamage(int amount)
        {
            RemoveHeartsInQuantity(amount);
        }
        
        private void WaveManager_OnAnyWaveSpawned()
        {
            UpdateDefeatedEnemiesCountText();
        }
    }
}