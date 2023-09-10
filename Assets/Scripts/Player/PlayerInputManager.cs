using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(PlayerAnimatorManager)
    )]
    public class PlayerInputManager : MonoBehaviour
    {
        public Vector2 MovementInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public bool Sprinting { get; private set; }
        public bool Jumping { get; private set; }
        public float MovementAmount => Mathf.Clamp01(Mathf.Abs(MovementInput.x) + Mathf.Abs(MovementInput.y));
        
        private PlayerInput _playerInput;
        private PlayerLocomotion _playerLocomotion;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerLocomotion = GetComponent<PlayerLocomotion>();

                _playerInput.PlayerMovement.Movement.performed += callbackContext => MovementInput = callbackContext.ReadValue<Vector2>();
                _playerInput.PlayerMovement.Camera.performed += callbackContext => CameraInput = callbackContext.ReadValue<Vector2>();
                
                _playerInput.PlayerActions.Sprint.performed += callbackContext => Sprinting = true;
                _playerInput.PlayerActions.Sprint.canceled += callbackContext => Sprinting = false;
                
                _playerInput.PlayerActions.Jump.performed += callbackContext => Jumping = true;
            }
        
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void HandleJumpingInput()
        {
            if (Jumping)
            {
                Jumping = false;

                _playerLocomotion.HandleJumping();
            }
        }
    }
}