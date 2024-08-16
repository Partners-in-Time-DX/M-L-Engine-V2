using UnityEngine;

namespace Player.Overworld.Mario.States.ActionStates.Jump
{
    public class MarioOverworldJumpState : MarioOverworldBaseState
    {
        private Vector3 _newMove;
        private RaycastHit _headContactCheck; // Check if Mario's head hits a surface
        private bool _isHeadHit;
        public MarioOverworldJumpState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory) : base(ctx, factory)
        {
        }

        public override void EnterState()
        {
            _isHeadHit = false;
            _ctx.IsJumping = true;
            _ctx.Velocity = _ctx.InitialJumpVelocity;
            _newMove = new Vector3(0f, 0f, 0f);
        }

        public override void UpdateState()
        {
            Debug.Log("Player Head Hit: " + _isHeadHit);
            _isHeadHit = CheckPlayerHeadHit();

            if (_isHeadHit)
            {
                _ctx.Velocity = 0f;
            }
            
            _ctx.CurrentAction.HandleAction();
            if (_ctx.CharacterMove.magnitude > 0.1f)
            {
                _movementHelper.HandleMovement(_newMove);
            }
            
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

        private bool CheckPlayerHeadHit()
        {
            Debug.DrawRay(
                new Vector3(
                    _ctx.transform.position.x, 
                    _ctx.transform.position.y + 2f, 
                    _ctx.transform.position.z), 
                Vector3.up, 
                Color.green);
            
            return Physics.Raycast(
                new Vector3(
                    _ctx.transform.position.x, 
                    _ctx.transform.position.y + 2f, 
                    _ctx.transform.position.z), 
                Vector3.up, 
                0.1f, 
                LayerMask.GetMask("Block"));
        }
    }
}