using Player.Factories;
using UnityEngine;

namespace Player.States
{
    public sealed class PlayerFallingState : PlayerBaseState
    {
        public PlayerFallingState(PlayerContext playerContext, PlayerStateFactory playerStateFactory) : base(playerContext, playerStateFactory) { }

        public override void OnEnter()
        {
            Debug.Log("[ENTERED] Falling state");
        }
        
        protected override void OnLeave()
        {
            Debug.Log("[LEAVING] Falling state");
        }

        protected override void CheckStateChange()
        {
            if (playerContext.PlayerStateApplicable.Grounded) 
            {
                ChangeState(playerStateFactory.Grounded());
            }
        }
    }
}