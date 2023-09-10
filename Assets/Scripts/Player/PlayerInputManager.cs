using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(PlayerInput),
        typeof(PlayerAnimatorManager)
    )]
    public class PlayerInputManager : MonoBehaviour
    {
        public Vector2 MovementInput => _movementInput;
        public Vector2 CameraInput => _cameraInput;
        public float MovementAmount => Mathf.Clamp01(Mathf.Abs(MovementInput.x) + Mathf.Abs(MovementInput.y));
    
        private PlayerInput _playerInput;
        
        private Vector2 _movementInput;
        private Vector2 _cameraInput;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();

                _playerInput.PlayerMovement.Movement.performed += callbackContext => _movementInput = callbackContext.ReadValue<Vector2>();
                _playerInput.PlayerMovement.Camera.performed += callbackContext => _cameraInput = callbackContext.ReadValue<Vector2>();
            }
        
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
    }
}