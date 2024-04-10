using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    public class GameOverUI : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private CanvasGroup _backgroundCanvasGroup;
        [SerializeField] private CanvasGroup _gameOverContainerCanvasGroup;
        
        [Space(10)]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {  
           _restartButton.onClick.AddListener(HandleRestartButton);
           _exitButton.onClick.AddListener(HandleExitButton);
           
           AnimateGameOverUI();
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(HandleRestartButton);
            _exitButton.onClick.RemoveListener(HandleExitButton);
        }

        private void AnimateGameOverUI()
        {
            Sequence gameOverUISequence = DOTween.Sequence();
            gameOverUISequence.Append(_backgroundCanvasGroup.DOFade(1f, 1f));
            gameOverUISequence.Append(_gameOverContainerCanvasGroup.DOFade(1f, 1f));
        }
        
        private void HandleRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void HandleExitButton()
        {
            Application.Quit();
        }
    }
}