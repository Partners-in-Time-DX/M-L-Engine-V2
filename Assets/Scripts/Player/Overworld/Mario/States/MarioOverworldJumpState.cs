using UnityEngine;

namespace Player.Overworld.Mario.States
{
    public class MarioOverworldJumpState : MarioOverworldBaseState
    {
        public MarioOverworldJumpState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory) : base(ctx, factory)
        {
        }

        public override void EnterState()
        {
            _ctx.IsJumping = true;
            _ctx.Velocity = _ctx.InitialJumpVelocity;
        }

        public override void UpdateState()
        {
            HandleGravity();
            TransitionToState();
        }

        public override void ExitState()
        {
            _ctx.IsJumping = false;
        }

        public override void TransitionToState()
        {
            if (_ctx.Velocity <= 0f)
            {
                SwitchStates(_factory.Falling());
            }
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_jump" + _ctx.Facing);
        }

        private void HandleGravity()
        {
            _ctx.Velocity += _ctx.Gravity * Time.deltaTime;
            _ctx.MarioController.Move(new Vector3(0f, _ctx.Velocity * Time.deltaTime));
        }
    }
}