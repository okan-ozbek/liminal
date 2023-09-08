using System.Collections.Generic;
using Player.Enums;
using Player.States;

namespace Player.Factories
{
    public sealed class PlayerStateFactory
    {
        private readonly Dictionary<PlayerStates, PlayerBaseState> _playerStates;

        public PlayerStateFactory(PlayerContext playerContext)
        {
            _playerStates = new Dictionary<PlayerStates, PlayerBaseState>
            {
                { PlayerStates.Grounded, new PlayerGroundedState(playerContext, this) },
                { PlayerStates.Jumping, new PlayerJumpingState(playerContext, this) },
                { PlayerStates.Falling, new PlayerFallingState(playerContext, this) }
            };
        }

        public PlayerBaseState Grounded()
        {
            return _playerStates[PlayerStates.Grounded];
        }
        
        public PlayerBaseState Jumping()
        {
            return _playerStates[PlayerStates.Jumping];
        }
        
        public PlayerBaseState Falling()
        {
            return _playerStates[PlayerStates.Falling];
        }
    }
}