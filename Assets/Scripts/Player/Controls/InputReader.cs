using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Controls
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameControls.IGameplayActions
    {
        private GameControls _gameControls;

        public event Action MousePerfomedEvent;
        public event Action MouseCancelledEvent;
        public Vector2 MousePosition => _gameControls.Gameplay.MousePosition.ReadValue<Vector2>();
        
        public void Disable()
        {
            _gameControls.Gameplay.Disable();
        }

        public void SetGameplay()
        {
            _gameControls.Gameplay.Enable();
        }
        
        private void OnEnable()
        {
            if (_gameControls != null)
                return;
            
            _gameControls = new GameControls();
            _gameControls.Gameplay.SetCallbacks(this);
            
            SetGameplay();
        }
        
        public void OnMousePosition(InputAction.CallbackContext context)
        {
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                MousePerfomedEvent?.Invoke();

            if (context.phase == InputActionPhase.Canceled)
                MouseCancelledEvent?.Invoke();
        }
    }
}