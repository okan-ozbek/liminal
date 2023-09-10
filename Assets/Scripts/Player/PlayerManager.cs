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
        [SerializeField]
        private CameraManager cameraManager;
        
        private PlayerInputManager _playerInputManager;
        private PlayerLocomotion _playerLocomotion;
        private PlayerAnimatorManager _playerAnimatorManager;
        
        private bool SetSprintAnimation => (_playerInputManager.Sprinting && _playerInputManager.MovementAmount >= PlayerAnimatorManager.StartRun);

        private void Awake()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }

        private void FixedUpdate()
        {
            _playerLocomotion.HandleLocomotionMovement();
            _playerAnimatorManager.UpdateAnimatorValues(0.0f, _playerInputManager.MovementAmount, SetSprintAnimation);
        }

        private void LateUpdate()
        {
            cameraManager.HandleCameraMovement();
        }
    }
}