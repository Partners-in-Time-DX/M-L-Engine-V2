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
            HandleGravity();
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
            
            if (!_ctx.IsGrounded)
            {
                SwitchStates(_factory.Falling());
            }
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_walk" + _ctx.Facing);
        }

        private void HandleGravity()
        {
            RaycastHit hit;
            if (Physics.Raycast(_ctx.transform.position, _ctx.transform.TransformDirection(Vector3.down), out hit, 0.5f))
            {
                _ctx.MarioController.Move(new Vector3(0f, _ctx.Velocity * Time.deltaTime));
            }
        }
    }
}