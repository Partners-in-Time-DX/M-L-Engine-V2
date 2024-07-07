using System.Collections.Generic;
using Player.Overworld.Enums;

namespace Player.Overworld.Mario.States
{
    public class MarioOverworldStateFactory
    {
        private MarioOverworldPlayerController _ctx;
        private Dictionary<PlayerOverworldStates, MarioOverworldBaseState> _states = 
            new Dictionary<PlayerOverworldStates, MarioOverworldBaseState>();

        public MarioOverworldStateFactory(MarioOverworldPlayerController ctx)
        {
            _ctx = ctx;

            _states[PlayerOverworldStates.IDLE] = new MarioOverworldIdleState(_ctx, this);
        }

        public MarioOverworldBaseState Idle()
        {
            return _states[PlayerOverworldStates.IDLE];
        }
    }
}