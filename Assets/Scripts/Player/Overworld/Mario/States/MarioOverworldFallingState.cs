using UnityEngine;

namespace Player.Overworld.Mario.States
{
    public class MarioOverworldFallingState : MarioOverworldBaseState
    {
        private Vector3 _newMove;
        public MarioOverworldFallingState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory) : base(ctx, factory)
        {
        }

        public override void EnterState()
        {
            _newMove = new Vector3(0f, 0f, 0f);
        }

        public override void UpdateState()
        {
                
            _movementHelper.HandleGravity();

            if (_ctx.CharacterMove.magnitude > 0.1f)
            {
                _movementHelper.HandleMovement(_newMove);
            }
            
            TransitionToState();
        }

        public override void ExitState()
        {
            
        }

        public override void TransitionToState()
        {
            if (_ctx.IsGrounded)
            {
                SwitchStates(_factory.Idle());
            } else if (_ctx.IsGrounded && _ctx.CharacterMove.magnitude >= 0.1f)
            {
                SwitchStates(_factory.Walking());
            }
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_fall" + _ctx.Facing);
        }
    }
}