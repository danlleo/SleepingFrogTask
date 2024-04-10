using Characters.Player;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent]
    public class UI : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private GameOverUI _gameOverUI;
        
        private void OnEnable()
        {
            PlayerHealth.OnAnyPlayerDied += PlayerHealth_OnAnyPlayerDied;
        }

        private void OnDisable()
        {
            PlayerHealth.OnAnyPlayerDied -= PlayerHealth_OnAnyPlayerDied;
        }

        private void PlayerHealth_OnAnyPlayerDied()
        {
            _gameplayUI.gameObject.SetActive(false);
            _gameOverUI.gameObject.SetActive(true);
        }
    }
}
