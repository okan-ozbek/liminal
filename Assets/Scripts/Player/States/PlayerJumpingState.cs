using Player.Factories;
using UnityEngine;

namespace Player.States
{
    public sealed class PlayerJumpingState : PlayerBaseState
    {
        public PlayerJumpingState(PlayerContext playerContext, PlayerStateFactory playerStateFactory) : base(playerContext, playerStateFactory) { }

        public override void OnEnter()
        {
            Debug.Log("[ENTERING] Jumping state");
            
            playerContext.rigidbody.AddForce(Vector3.up * playerContext.jumpForce, ForceMode.Impulse);
        }

        protected override void OnLeave()
        {
            Debug.Log("[LEAVING] Jumping state");
        }

        protected override void CheckStateChange()
        {
            if (playerContext.PlayerStateApplicable.Falling)
            {
                ChangeState(playerStateFactory.Falling());
            }
        }
    }
}