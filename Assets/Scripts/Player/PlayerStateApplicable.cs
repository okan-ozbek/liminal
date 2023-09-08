namespace Player
{
    public sealed class PlayerStateApplicable
    {
        private readonly PlayerContext _playerContext;
        
        public PlayerStateApplicable(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public bool Grounded => (_playerContext.Grounded() || _playerContext.NoVerticalVelocity());
        
        public bool Jumping => (_playerContext.Grounded() && _playerContext.PlayerInput.PressedJump);
        
        public bool Falling => (!_playerContext.Grounded() && _playerContext.FallingDown());
    }
}