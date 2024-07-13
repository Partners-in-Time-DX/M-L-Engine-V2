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
            float rotateAngle = Mathf.Atan2(_ctx.CharacterMove.x, _ctx.CharacterMove.y) * Mathf.Rad2Deg + _ctx.Cam.eulerAngles.y;
            _ctx.transform.rotation = Quaternion.Euler(0f, rotateAngle, 0f);
            
            _newMove = Quaternion.Euler(0f, rotateAngle, 0f) * Vector3.forward;

            if (_ctx.CharacterMove.magnitude > 0.1f)
            {
                _ctx.MoveAngle = rotateAngle;
            
                _newMove = _newMove * _ctx.MoveSpeed * Time.deltaTime;
            
                _ctx.MarioController.Move(_newMove);      
            }
            
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
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_walk" + _ctx.Facing);
        }
    }
}