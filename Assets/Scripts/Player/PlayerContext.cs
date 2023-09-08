using Player.Factories;
using Player.States;
using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(Rigidbody),
        typeof(BoxCollider)
    )]
    public sealed class PlayerContext : MonoBehaviour
    {
        public float runSpeed;
        public float walkSpeed;
        public float jumpForce;

        public float Speed => (PlayerInput.PressedSprint)
            ? runSpeed
            : walkSpeed;

        public new Rigidbody rigidbody;
        public BoxCollider boxCollider;

        public PlayerBaseState playerState;
        public PlayerInput PlayerInput { get; private set; }
        public PlayerStateApplicable PlayerStateApplicable { get; private set; }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();

            PlayerInput = new PlayerInput();
            PlayerStateApplicable = new PlayerStateApplicable(this);

            PlayerStateFactory playerStateFactory = new PlayerStateFactory(this);

            playerState = playerStateFactory.Falling();
            playerState.OnEnter();
        }

        private void Update()
        {
            PlayerInput.OnUpdate();
            playerState.OnUpdate();
        }

        private void FixedUpdate()
        {
            playerState.OnFixedUpdate();
        }

        public bool Grounded()
        {
            const float offset = 0.2f;

            float length = boxCollider.transform.localScale.y * 0.5f;

            return (Physics.Raycast(transform.position, Vector3.down, length + offset));
        }

        public bool NotGrounded()
        {
            return (!Grounded());
        }

        public bool FallingDown()
        {
            return (rigidbody.velocity.y < 0.0f);
        }

        public bool RisingUp()
        {
            return (rigidbody.velocity.y > 0.0f);
        }

        public bool NoVerticalVelocity()
        {
            return (rigidbody.velocity.y == 0.0f);
        }
    }
}
