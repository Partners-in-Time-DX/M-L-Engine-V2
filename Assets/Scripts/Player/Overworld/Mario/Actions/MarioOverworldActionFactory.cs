using System;
using System.Collections.Generic;
using System.Linq;
using ErrorHandling.Exceptions;
using Player.Overworld.Enums;

namespace Player.Overworld.Mario.States
{
    public class MarioOverworldActionFactory
    {
        private MarioOverworldPlayerController _ctx;
        private readonly Dictionary<PlayerOverworldActions, MarioOverworldBaseAction> _actions = 
            new Dictionary<PlayerOverworldActions, MarioOverworldBaseAction>();

        public MarioOverworldActionFactory(MarioOverworldPlayerController ctx)
        {
            _ctx = ctx;

            _actions[PlayerOverworldActions.JUMP] = new MarioOverworldJumpAction(_ctx, this);
            _actions[PlayerOverworldActions.HAMMER] = new MarioOverworldHammerAction(_ctx, this);
        }

        public MarioOverworldBaseAction GetCurrentAction(PlayerOverworldActions action)
        {
            switch (action)
            {
                case PlayerOverworldActions.JUMP:
                    return _actions[PlayerOverworldActions.JUMP];
                case PlayerOverworldActions.HAMMER:
                    return _actions[PlayerOverworldActions.HAMMER];
                default:
                    throw new InvalidPlayerActionException($"The provided action of {nameof(action)} is invalid!");
            }
        }

        public PlayerOverworldActions[] ActionsToArray()
        {
            return Enum.GetValues(typeof(PlayerOverworldActions)).Cast<PlayerOverworldActions>().ToArray();
        }
    }
}