using System;
using Enemies.Overworld.Types;
using ErrorHandling.Exceptions;
using UnityEngine;

namespace Enemies.Overworld.States
{
    public abstract class EnemyOverworldBaseState
    {
        protected AbstractOverworldEnemy _ctx;
        protected EnemyOverworldStateFactory _factory;

        public EnemyOverworldBaseState(AbstractOverworldEnemy ctx, EnemyOverworldStateFactory factory)
        {
            _ctx = ctx;
            _factory = factory;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void TransitionToState();
        public abstract void AnimateState();

        protected void SwitchStates(EnemyOverworldBaseState newState)
        {
            if (newState != null)
            {
                try
                {
                    ExitState();
                    newState.EnterState();
                    _ctx.CurrentState = newState;
                }
                catch (InvalidEnemyStateException e)
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