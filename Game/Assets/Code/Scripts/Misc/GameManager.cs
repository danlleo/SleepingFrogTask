using UI;
using UnityEngine;
using Wave;
using Zenject;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        private WaveManager _waveManager;
    
        [Inject]
        private void Construct(WaveManager waveManager)
        {
            _waveManager = waveManager;
        }

        private void OnEnable()
        {
            WaveUI.OnAnyFinishedAnimateNewWave += WaveUI_OnAnyFinishedAnimateNewWave;
        }

        private void OnDisable()
        {
            WaveUI.OnAnyFinishedAnimateNewWave -= WaveUI_OnAnyFinishedAnimateNewWave;
        }

        private void WaveUI_OnAnyFinishedAnimateNewWave()
        {
            _waveManager.SpawnWave(this);
        }
    }
}
