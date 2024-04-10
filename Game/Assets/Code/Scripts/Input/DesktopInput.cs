using System;
using UnityEngine.InputSystem;

namespace Input
{
    public class DesktopInput : IInput, IDisposable
    {
        public event Action OnLeftHooked;
        public event Action OnRightHooked;

        private readonly PlayerControls _playerControls;

        public DesktopInput()
        {
            _playerControls = new PlayerControls();
            _playerControls.Gameplay.Enable();
            _playerControls.Gameplay.LeftHook.performed += PlayerControls_OnLeftHooked; 
            _playerControls.Gameplay.RightHook.performed += PlayerControls_OnRightHooked;
        }

        public void Dispose()
        {
            _playerControls.Gameplay.LeftHook.performed -= PlayerControls_OnLeftHooked;
            _playerControls.Gameplay.RightHook.performed -= PlayerControls_OnRightHooked;
        }
        
        private void PlayerControls_OnLeftHooked(InputAction.CallbackContext obj)
        {
            OnLeftHooked?.Invoke();
        }
        
        private void PlayerControls_OnRightHooked(InputAction.CallbackContext obj)
        {
            OnRightHooked?.Invoke();
        }
    }
}