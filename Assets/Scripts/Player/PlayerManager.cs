using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(InputManager),
        typeof(PlayerLocomotion)
    )]
    public class PlayerManager : MonoBehaviour
    {
        private InputManager _inputManager;
        private PlayerLocomotion _playerLocomotion;
        
        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        private void FixedUpdate()
        {
            _playerLocomotion.HandleAllMovement();
        }
    }
}