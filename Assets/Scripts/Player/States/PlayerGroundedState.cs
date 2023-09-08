using Player.Factories;
using UnityEngine;

namespace Player.States
{
    public sealed class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerContext playerContext, PlayerStateFactory playerStateFactory) : base(playerContext, playerStateFactory) { }

        public override void OnEnter()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            playerContext.rigidbody.AddForce(playerContext.playerInput.MovementDirection * playerContext.Speed, ForceMode.Force);
        }

        protected override void OnLeave()
        {
            // throw new System.NotImplementedException();
        }

        protected override void CheckStateChange()
        {
            if (playerContext.Grounded() && playerContext.playerInput.PressedJump)
            {   
                ChangeState(playerStateFactory.Jumping());
            }

            if (!playerContext.Grounded() && playerContext.rigidbody.velocity.y < 0.0f)
            {
                ChangeState(playerStateFactory.Falling());
            }
        }
    }
}