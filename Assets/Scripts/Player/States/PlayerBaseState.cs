using Player.Factories;

namespace Player.States
{
    public abstract class PlayerBaseState
    {
        protected readonly PlayerContext playerContext;
        protected readonly PlayerStateFactory playerStateFactory;

        protected PlayerBaseState(PlayerContext playerContext, PlayerStateFactory playerStateFactory)
        {
            this.playerContext = playerContext;
            this.playerStateFactory = playerStateFactory;
        }

        public abstract void OnEnter();

        public virtual void OnUpdate()
        {
            CheckStateChange();
        }

        public virtual void OnFixedUpdate()
        {
            // No basic implmentation..
        }

        protected abstract void OnLeave();

        protected abstract void CheckStateChange();

        public void ChangeState(PlayerBaseState state)
        {
            OnLeave();

            playerContext.playerState = state;

            state.OnEnter();
        }
    }
}