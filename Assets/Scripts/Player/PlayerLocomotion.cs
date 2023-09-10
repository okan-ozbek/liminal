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
        public float movementSpeed = 7.0f;
        public float rotationSpeed = 0.3f;
        
        private PlayerInputManager _inputManager;
        
        private Vector3 _movementDirection;
        private Vector3 _targetDirection;
        private Transform _camera;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
            _rigidbody = GetComponent<Rigidbody>();

            if (Camera.main != null) _camera = Camera.main.transform;
        }

        public void HandleLocomotionMovement()
        {
            HandleMovement();
            HandleRotation();
        }
        
        private void HandleMovement()
        {
            _movementDirection = _camera.forward * _inputManager.MovementInput.y;
            _movementDirection += _camera.right * _inputManager.MovementInput.x;
            _movementDirection.y = 0.0f;
            
            _movementDirection.Normalize();
            _movementDirection *= movementSpeed;

            Vector3 movementVelocity = _movementDirection;
            _rigidbody.velocity = movementVelocity;
        }

        private void HandleRotation()
        {
            _targetDirection = _camera.forward * _inputManager.MovementInput.y;
            _targetDirection += _camera.right * _inputManager.MovementInput.x;
            _targetDirection.y = 0.0f;

            _targetDirection.Normalize();

            if (_targetDirection == Vector3.zero)
            {
                _targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

            transform.rotation = rotation;
        }
    }
}
