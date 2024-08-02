using System;
using UnityEngine;

namespace Player.Overworld.Mario.States
{
    public class MarioOverworldWalkingState : MarioOverworldBaseState
    {
        private Vector3 _newMove;
        public MarioOverworldWalkingState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory) : base(ctx, factory)
        {
        }

        public override void EnterState()
        {
            _newMove = new Vector3(0f, 0f, 0f);
        }

        public override void UpdateState()
        {
            _movementHelper.HandleMovement(_newMove);
            
            TransitionToState();
        }

        public override void ExitState()
        {
            
        }

        public override void TransitionToState()
        {
            if (_ctx.CharacterMove.magnitude < 0.1f)
            {
                SwitchStates(_factory.Idle());
            }
            
            if (!_ctx.IsGrounded && !_ctx.IsJumping)
            {
                SwitchStates(_factory.Falling());
            }
            
            if (_ctx.MarioAction.triggered && _ctx.IsGrounded)
            {
                SwitchStates(_actionStateHelper.GetActionState(_ctx.CurrentAction));
            }
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_walk" + _ctx.Facing);
        }
    }
}