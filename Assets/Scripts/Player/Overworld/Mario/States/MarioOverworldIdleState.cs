using UnityEngine;

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
            if (_ctx.CharacterMove.magnitude > 0.1f)
            {
                SwitchStates(_factory.Walking());
            }
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.Play("m_stand" + _ctx.Facing);
        }
    }
}