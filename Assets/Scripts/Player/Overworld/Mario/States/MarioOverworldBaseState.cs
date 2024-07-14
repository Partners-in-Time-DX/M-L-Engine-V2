using System;
using ErrorHandling.Exceptions;
using Player.Overworld.Mario.Helpers;
using UnityEngine;

namespace Player.Overworld.Mario.States
{
    public abstract class MarioOverworldBaseState
    {
        protected MarioOverworldPlayerController _ctx;
        protected MarioOverworldStateFactory _factory;
        protected MovementHelper _movementHelper;

        public MarioOverworldBaseState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory)
        {
            _ctx = ctx;
            _factory = factory;
            _movementHelper = new MovementHelper(_ctx);
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void TransitionToState();
        public abstract void AnimateState();

        protected void SwitchStates(MarioOverworldBaseState newState)
        {
            if (newState != null)
            {
                try
                {
                    ExitState();
                    newState.EnterState();
                    _ctx.CurrentState = newState;
                }
                catch (InvalidMarioStateException e)
                {
                    Debug.LogError(e);
                    throw;
                }
            }
            else
            {
                Debug.LogError("No state was provided in the SwitchStates method.");
                throw new NullReferenceException("No state was provided in the SwitchStates method.");
            }
        }
    }
}