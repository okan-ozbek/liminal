using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Range = Structs.Range;

namespace Player
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInputManager;
        
        [Header("Game Objects")]
        public Transform target;
        public Transform cameraPivot;

        [Header("General")]
        public float cameraFollowSpeed = 0.2f;
        public float sensitivity = 0.5f;
        
        [Header("Collision")]
        public float cameraCollisionRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;
        public LayerMask collisionLayers;
        
        private float _lookAngle;
        private float _pivotAngle;
        private Vector3 _defaultPosition;
        private Vector3 _cameraFollowVelocity;
        private Vector3 _cameraPosition;
        private Transform _mainCamera;

        private readonly Range _pivotAngleRange = new(-35.0f, 60.0f);

        private void Awake()
        {
            if (Camera.main != null)
            {
                _mainCamera = Camera.main.transform;
                _defaultPosition = _mainCamera.position;
            }
        }
        
        public void HandleCameraMovement()
        {
            FollowTarget();
            RotateCamera();
            CameraCollision();
        }
        
        private void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(transform.position, target.position, ref _cameraFollowVelocity, cameraFollowSpeed);

            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            _lookAngle += (playerInputManager.CameraInput.x * sensitivity);
            _pivotAngle -= (playerInputManager.CameraInput.y * sensitivity);

            _pivotAngle = Mathf.Clamp(_pivotAngle, _pivotAngleRange.min, _pivotAngleRange.max);

            Vector3 rotation = Vector3.up * _lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.right * _pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }

        private void CameraCollision()
        {
            Vector3 targetPosition = _defaultPosition;
            Vector3 direction = _mainCamera.position - cameraPivot.position;
            
            direction.Normalize();

            if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out var hit, Mathf.Abs(targetPosition.z), collisionLayers))
            {
                float distance = Vector3.Distance(cameraPivot.position, hit.point);
                
                targetPosition.z = -(distance - cameraCollisionOffset);
            }

            if (Mathf.Abs(targetPosition.z) < minimumCollisionOffset)
            {
                targetPosition.z -= minimumCollisionOffset;
            }

            _cameraPosition.z = Mathf.Lerp(_mainCamera.localPosition.z, targetPosition.z, 0.2f);
            _mainCamera.localPosition = _cameraPosition;
        }
    }
}
