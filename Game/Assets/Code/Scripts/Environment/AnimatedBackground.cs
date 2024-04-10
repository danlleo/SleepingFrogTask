using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Environment
{
    public class AnimatedBackground : MonoBehaviour
    {
        [Header("External references")] 
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite[] _sprites;
    
        [Header("Settings")]
        [SerializeField] private float _changeIntervalInSeconds = 0.5f;

        private int _currentSpriteIndex;
        private float _timer;
    
        private void Start()
        {
            TurnOnBackground();
            InitializeSprite();
            StartCoroutine(AnimateBackgroundRoutine());
        }

        private void TurnOnBackground()
        {
            _backgroundImage.enabled = true;
        }

        private void InitializeSprite()
        {
            if (_sprites.Length > 0)
            {
                _backgroundImage.sprite = _sprites[0];
            }
        }

        private IEnumerator AnimateBackgroundRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_changeIntervalInSeconds);

                _currentSpriteIndex++;
            
                if (_currentSpriteIndex >= _sprites.Length)
                {
                    _currentSpriteIndex = 0;
                }

                _backgroundImage.sprite = _sprites[_currentSpriteIndex];
            }
        }
    }
}

