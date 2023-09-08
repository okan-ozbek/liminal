using Player.Factories;
using UnityEngine;

namespace Player.States
{
    public sealed class PlayerJumpingState : PlayerBaseState
    {
        public PlayerJumpingState(PlayerContext playerContext, PlayerStateFactory playerStateFactory) : base(playerContext, playerStateFactory) { }

        public override void OnEnter()
        {
            playerContext.rigidbody.AddForce(Vector3.up * playerContext.jumpForce, ForceMode.Impulse);
        }

        protected override void OnLeave()
        {
            // throw new System.NotImplementedException();
        }

        protected override void CheckStateChange()
        {
            if (playerContext.Grounded()) 
            {
                ChangeState(playerStateFactory.Grounded());
            }
            
            if (!playerContext.Grounded() && playerContext.rigidbody.velocity.y < 0.0f)
            {
                ChangeState(playerStateFactory.Falling());
            }
        }
    }
}