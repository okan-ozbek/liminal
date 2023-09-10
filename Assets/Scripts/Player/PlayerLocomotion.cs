using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(PlayerInputManager),
        typeof(Rigidbody),
        typeof(CapsuleCollider)
    )]
    public class PlayerLocomotion : MonoBehaviour
    {
        [Header("Movement Speeds")]
        public float walkSpeed = 1.5f;
        public float runSpeed = 5.0f;
        public float sprintSpeed = 7.0f;
        public float rotationSpeed = 0.3f;
        public float movementSpeedDebug;
        
        public float MovementSpeed => GetMovementSpeed();

        private PlayerInputManager _inputManager;
        
        private Vector3 _movementDirection;
        private Vector3 _targetDirection;
        private Transform _mainCamera;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
            _rigidbody = GetComponent<Rigidbody>();

            if (Camera.main != null)
            {
                _mainCamera = Camera.main.transform;
            }
        }

        public void HandleLocomotionMovement()
        {
            HandleMovement();
            HandleRotation();

            movementSpeedDebug = MovementSpeed;
        }
        
        private void HandleMovement()
        {
            _movementDirection = _mainCamera.forward * _inputManager.MovementInput.y;
            _movementDirection += _mainCamera.right * _inputManager.MovementInput.x;
            
            _movementDirection.Normalize();
            
            _movementDirection.y = 0.0f;
            
            _movementDirection *= MovementSpeed;

            Vector3 movementVelocity = _movementDirection;
            _rigidbody.velocity = movementVelocity;
        }

        private void HandleRotation()
        {
            _targetDirection = _mainCamera.forward * _inputManager.MovementInput.y;
            _targetDirection += _mainCamera.right * _inputManager.MovementInput.x;
            
            _targetDirection.Normalize();
            
            _targetDirection.y = 0.0f;

            if (_targetDirection == Vector3.zero)
            {
                _targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

            transform.rotation = rotation;
        }

        private float GetMovementSpeed()
        {
            if (!(_inputManager.MovementAmount >= PlayerAnimatorManager.StartRun))
            {
                return walkSpeed;
            }
            
            return _inputManager.Sprinting 
                ? sprintSpeed 
                : runSpeed;
        }
    }
}
