using UnityEngine;

namespace Player.Overworld.Mario.States
{
    public class MarioOverworldFallingState : MarioOverworldBaseState
    {
        public MarioOverworldFallingState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory) : base(ctx, factory)
        {
        }

        public override void EnterState()
        {
            
        }

        public override void UpdateState()
        {
                
            HandleGravity();
            TransitionToState();
        }

        private void HandleGravity()
        {
            float prevVel = _ctx.Velocity;
            _ctx.Velocity += _ctx.Gravity * Time.deltaTime * _ctx.FallMultiplier;
            float avgVel = (prevVel + _ctx.Velocity) / 2;
            _ctx.MarioController.Move(new Vector3(0f, avgVel * Time.deltaTime));   
        }

        public override void ExitState()
        {
            
        }

        public override void TransitionToState()
        {
            if (_ctx.MarioController.isGrounded)
            {
                SwitchStates(_factory.Idle());
            }
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_fall" + _ctx.Facing);
        }
    }
}