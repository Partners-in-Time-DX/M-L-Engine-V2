using System;
using ErrorHandling.Exceptions;
using Player.Overworld.Mario.Actions;
using Player.Overworld.Mario.States;
using UnityEngine;

namespace Player.Overworld.Mario.Helpers
{
    public class ActionStateHelper
    {
        private static MarioOverworldStateFactory _stateFactory;

        public ActionStateHelper(MarioOverworldStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        
        public MarioOverworldBaseState GetActionState(MarioOverworldBaseAction action)
        {
            if (action is MarioOverworldJumpAction)
            {
                return _stateFactory.Jumping();
            }
            else if (action is MarioOverworldHammerAction)
            {
                return _stateFactory.Hammer();
            }

            throw new InvalidPlayerActionException($"Invalid MarioOverworldBaseAction: {action.GetType()} passed in GetActionState.");
        }
    }
}