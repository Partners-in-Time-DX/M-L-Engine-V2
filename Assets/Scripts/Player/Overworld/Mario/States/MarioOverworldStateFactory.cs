using System.Collections.Generic;
using Player.Overworld.Enums;
using Player.Overworld.Mario.States.ActionStates.Hammer;
using Player.Overworld.Mario.States.ActionStates.Jump;

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
            _states[PlayerOverworldStates.WALKING] = new MarioOverworldWalkingState(_ctx, this);
            _states[PlayerOverworldStates.FALLING] = new MarioOverworldFallingState(_ctx, this);
            _states[PlayerOverworldStates.JUMPING] = new MarioOverworldJumpState(_ctx, this);
            _states[PlayerOverworldStates.HAMMER] = new MarioOverworldHammerState(_ctx, this);
        }

        public MarioOverworldBaseState Idle()
        {
            return _states[PlayerOverworldStates.IDLE];
        }

        public MarioOverworldBaseState Walking()
        {
            return _states[PlayerOverworldStates.WALKING];
        }
        
        public MarioOverworldBaseState Falling()
        {
            return _states[PlayerOverworldStates.FALLING];
        }

        public MarioOverworldBaseState Jumping()
        {
            return _states[PlayerOverworldStates.JUMPING];
        }
        
        public MarioOverworldBaseState Hammer()
        {
            return _states[PlayerOverworldStates.HAMMER];
        }
    }
}