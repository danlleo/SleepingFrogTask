using System;
using DG.Tweening;
using Sound;
using UnityEngine;
using UnityEngine.UI;
using Wave;

namespace UI
{
    [DisallowMultipleComponent]
    public class WaveUI : MonoBehaviour
    {
        public static Action OnAnyFinishedAnimateNewWave;
        
        [Header("External references")]
        [SerializeField] private CanvasGroup _newWaveBackgroundCanvasGroup;
        [SerializeField] private CanvasGroup _nextWaveTextCanvasGroup;

        [Space(10)] 
        [SerializeField] private CanvasGroup _clearedAllWavesBackgroundCanvasGroup;
        [SerializeField] private CanvasGroup _clearedAllWavesTextCanvasGroup;
        [SerializeField] private CanvasGroup _exitButtonCanvasGroup;
        
        [Space(10)]
        [SerializeField] private Button _exitButton;

        [Space(10)]
        [SerializeField] private AudioClip _newWaveAudioClip;
        
        private void OnEnable()
        {
            _exitButton.onClick.AddListener(HandleExitButton);
            WaveManager.OnAnyWaveCleared += WaveManager_OnAnyWaveCleared;
            WaveManager.OnAnyClearedAllWaves += WaveManager_OnAnyClearedAllWaves;
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(HandleExitButton);
            WaveManager.OnAnyWaveCleared -= WaveManager_OnAnyWaveCleared;
            WaveManager.OnAnyClearedAllWaves -= WaveManager_OnAnyClearedAllWaves;
        }

        private void Start()
        {
            AnimateNewWaveIncomingUIElement();
        }

        private void AnimateNewWaveIncomingUIElement()
        {
            SoundFXManager.Instance.PlaySoundFX2DClip(_newWaveAudioClip, .4f);
            
            Sequence newWaveIncomingSequence = DOTween.Sequence();
            newWaveIncomingSequence.Append(_newWaveBackgroundCanvasGroup.DOFade(1f, 1f));
            newWaveIncomingSequence.Append(_nextWaveTextCanvasGroup.DOFade(1f, 1f));
            newWaveIncomingSequence.AppendInterval(2f);
            newWaveIncomingSequence.Append(_nextWaveTextCanvasGroup.DOFade(0f, .5f));
            newWaveIncomingSequence.Append(_newWaveBackgroundCanvasGroup.DOFade(0f, .5f));
            newWaveIncomingSequence.OnComplete(() => OnAnyFinishedAnimateNewWave?.Invoke());
        }
        
        private void AnimateClearedAllWavesUIElement()
        {
            Sequence clearedAllWavesSequence = DOTween.Sequence();
            clearedAllWavesSequence.Append(_clearedAllWavesBackgroundCanvasGroup.DOFade(1f, 1f));
            clearedAllWavesSequence.Append(_clearedAllWavesTextCanvasGroup.DOFade(1f, 1f));
            clearedAllWavesSequence.Append(_exitButtonCanvasGroup.DOFade(1f, 1f));
        }

        private void HandleExitButton()
        {
            Application.Quit();
        }
        
        private void WaveManager_OnAnyWaveCleared()
        {
            AnimateNewWaveIncomingUIElement();
        }
        
        private void WaveManager_OnAnyClearedAllWaves()
        {
            AnimateClearedAllWavesUIElement();
        }
    }
}
