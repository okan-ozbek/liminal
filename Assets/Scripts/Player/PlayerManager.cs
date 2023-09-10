using Player.Enums;
using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(PlayerInputManager),
        typeof(PlayerLocomotion),
        typeof(PlayerAnimatorManager)
    )]
    public class PlayerManager : MonoBehaviour
    {
        public bool isInteracting;
        
        [SerializeField]
        private CameraManager cameraManager;

        private Animator _animator;
        private PlayerInputManager _playerInputManager;
        private PlayerLocomotion _playerLocomotion;
        private PlayerAnimatorManager _playerAnimatorManager;
        
        private bool SetSprintAnimation => (_playerInputManager.Sprinting && _playerInputManager.MovementAmount >= PlayerAnimatorManager.StartRun);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerInputManager = GetComponent<PlayerInputManager>();
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        private void Update()
        {
            _playerInputManager.HandleJumpingInput();
        }

        private void FixedUpdate()
        {
            _playerLocomotion.HandleLocomotionMovement();
            _playerAnimatorManager.UpdateAnimatorValues(0.0f, _playerInputManager.MovementAmount, SetSprintAnimation);
        }

        private void LateUpdate()
        {
            cameraManager.HandleCameraMovement();

            isInteracting = _animator.GetBool(AnimatorEnum.isInteracting.ToString());
            _playerLocomotion.isJumping = _animator.GetBool(AnimatorEnum.isJumping.ToString());
            _animator.SetBool(AnimatorEnum.isGrounded.ToString(), _playerLocomotion.isGrounded);
        }
    }
}