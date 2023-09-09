using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(InputManager),
        typeof(Rigidbody),
        typeof(CapsuleCollider)
    )]
    public class PlayerLocomotion : MonoBehaviour
    {
        public float movementSpeed = 7.0f;
        public float rotationSpeed = 0.3f;
        
        private InputManager _inputManager;
        
        private Vector3 _movementDirection;
        private Vector3 _targetDirection;
        private Transform _camera;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
            _rigidbody = GetComponent<Rigidbody>();

            if (Camera.main != null) _camera = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleMovement();
            HandleRotation();
        }
        
        private void HandleMovement()
        {
            var invertedCameraPosition = _camera.transform.position * -1;

            _movementDirection = new Vector3(
                invertedCameraPosition.x * _inputManager.HorizontalMovementInput,
                0.0f,
                invertedCameraPosition.z * _inputManager.VerticalMovementInput
            );
            
            _movementDirection.Normalize();
            _movementDirection *= movementSpeed;

            Vector3 movementVelocity = _movementDirection;
            _rigidbody.velocity = movementVelocity;
        }

        private void HandleRotation()
        {
            var invertedCameraPosition = _camera.transform.position * -1;

            _targetDirection = new Vector3(
                invertedCameraPosition.x * _inputManager.HorizontalMovementInput,
                0.0f,
                invertedCameraPosition.z = _inputManager.VerticalMovementInput
            );
            
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
