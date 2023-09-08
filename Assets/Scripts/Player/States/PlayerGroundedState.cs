using Player.Factories;
using UnityEngine;

namespace Player.States
{
    public sealed class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerContext playerContext, PlayerStateFactory playerStateFactory) : base(playerContext, playerStateFactory) { }

        public override void OnEnter()
        {
            Debug.Log("[ENTERED] Grounded state");
        }

        public override void OnFixedUpdate()
        {
            playerContext.rigidbody.AddForce(playerContext.PlayerInput.MovementDirection * playerContext.Speed, ForceMode.Force);
        }

        protected override void OnLeave()
        {
            Debug.Log("[LEAVING] Grounded state");
        }

        protected override void CheckStateChange()
        {
            if (playerContext.PlayerStateApplicable.Jumping)
            {   
                ChangeState(playerStateFactory.Jumping());
            }

            if (playerContext.PlayerStateApplicable.Falling)
            {
                ChangeState(playerStateFactory.Falling());
            }
        }
    }
}