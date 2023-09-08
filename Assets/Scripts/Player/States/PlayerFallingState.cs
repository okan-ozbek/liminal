using Player.Factories;

namespace Player.States
{
    public sealed class PlayerFallingState : PlayerBaseState
    {
        public PlayerFallingState(PlayerContext playerContext, PlayerStateFactory playerStateFactory) : base(playerContext, playerStateFactory) { }

        public override void OnEnter()
        {
            // throw new System.NotImplementedException();
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
        }
    }
}