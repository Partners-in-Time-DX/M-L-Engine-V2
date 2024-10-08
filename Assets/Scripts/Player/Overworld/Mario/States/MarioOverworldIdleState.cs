using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Player.Overworld.Mario.States
{
    public class MarioOverworldIdleState : MarioOverworldBaseState
    {
        public MarioOverworldIdleState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory) : base(ctx, factory)
        {
        }

        public override void EnterState()
        {
            _ctx.transform.rotation = Quaternion.Euler(0f, _ctx.MoveAngle, 0f); // Sets rotation of object if it was modified in any other state
            _ctx.CharacterMove = Vector3.zero;
            _ctx.Velocity = 0f; // Reset velocity to zero when transitioning from another state, else the player will fall too quickly
        }

        public override void UpdateState()
        {
            TransitionToState();
        }

        public override void ExitState()
        {

        }

        public override void TransitionToState()
        {
            if (!_ctx.IsGrounded && !_ctx.IsJumping)
            {
                SwitchStates(_factory.Falling());
            }
            
            if (_ctx.CharacterMove.magnitude > 0.1f)
            {
                SwitchStates(_factory.Walking());
            }

            if (_ctx.MarioAction.triggered && _ctx.IsGrounded)
            {
                MarioOverworldBaseState actionState = _actionStateHelper.GetActionState(_ctx.CurrentAction);
                SwitchStates(actionState);
            }
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_stand" + _ctx.Facing);
        }
    }
}