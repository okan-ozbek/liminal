using Player.Enums;
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
        private PlayerManager _playerManager;
        private PlayerAnimatorManager _playerAnimatorManager;

        [Header("Falling")]
        public bool isGrounded;
        public float inAirTimer;
        public float leapingVelocity;
        public float fallingVelocity;
        public LayerMask environmentLayer;
        public float raycastOffset;

        [Header("Movement Speeds")]
        public float walkSpeed = 1.5f;
        public float runSpeed = 5.0f;
        public float sprintSpeed = 7.0f;
        public float rotationSpeed = 0.3f;
        public float movementSpeedDebug;

        [Header("Jumping")] 
        public float jumpHeight = 3.0f;
        public float gravityIntensity = -15.0f;
        public bool isJumping;
        
        public float MovementSpeed => GetMovementSpeed();

        private PlayerInputManager _inputManager;
        
        private Vector3 _movementDirection;
        private Vector3 _targetDirection;
        private Transform _mainCamera;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            _playerManager = GetComponent<PlayerManager>();
            _inputManager = GetComponent<PlayerInputManager>();
            _rigidbody = GetComponent<Rigidbody>();

            if (Camera.main != null)
            {
                _mainCamera = Camera.main.transform;
            }
        }

        public void HandleLocomotionMovement()
        {
            HandleFallingAndLanding();
            
            if (_playerManager.isInteracting)
            {
                return;
            }
            
            HandleMovement();
            HandleRotation();

            movementSpeedDebug = MovementSpeed;
        }
        
        private void HandleMovement()
        {
            if (isJumping)
                return;
            
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
            if (isJumping)
                return;
            
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

        private void HandleFallingAndLanding()
        {
            Vector3 raycastOrigin = transform.position;
            raycastOrigin.y = raycastOrigin.y + raycastOffset;

            if (!isGrounded && !isJumping)
            {
                if (!_playerManager.isInteracting)
                {
                    _playerAnimatorManager.PlayTargetAnimation(AnimationEnum.Falling.ToString(), true);
                }

                inAirTimer = inAirTimer + Time.deltaTime;
                _rigidbody.AddForce(transform.forward * leapingVelocity);
                _rigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
            }

            if (Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out RaycastHit hit, 1.0f, environmentLayer))
            {
                if (!isGrounded && _playerManager.isInteracting)
                {   
                    _playerAnimatorManager.PlayTargetAnimation(AnimationEnum.Landing.ToString(), true);
                }

                inAirTimer = 0;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        public void HandleJumping()
        {
            if (isGrounded)
            {
                _playerAnimatorManager.Animator.SetBool(AnimatorEnum.isJumping.ToString(), true);
                _playerAnimatorManager.PlayTargetAnimation(AnimationEnum.Jumping.ToString(), true);

                float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
                Vector3 playerVelocity = _movementDirection;
                playerVelocity.y = jumpingVelocity;

                _rigidbody.velocity = playerVelocity;
            }
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
