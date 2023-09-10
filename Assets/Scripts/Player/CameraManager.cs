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
        
        public Transform target;
        public Transform cameraPivot;
        
        public float cameraFollowSpeed = 0.2f;
        [Range(0, 5)] public float sensitivity = 0.5f;

        private Vector3 _cameraFollowVelocity = Vector3.zero;

        private float _lookAngle;
        private float _pivotAngle;

        private readonly Range _pivotAngleRange = new(-35.0f, 60.0f);

        public void HandleCameraMovement()
        {
            FollowTarget();
            RotateCamera();
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

        private void Collision()
        {

        }
    }
}
