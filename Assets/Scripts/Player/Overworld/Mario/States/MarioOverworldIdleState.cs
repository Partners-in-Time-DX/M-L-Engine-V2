namespace Player.Overworld.Mario.States
{
    public class MarioOverworldIdleState : MarioOverworldBaseState
    {
        public MarioOverworldIdleState(MarioOverworldPlayerController ctx, MarioOverworldStateFactory factory) : base(ctx, factory)
        {
        }

        public override void EnterState()
        {

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
            /*if (expr)
            {
                
            }*/
        }

        public override void AnimateState()
        {
            _ctx.MarioAnimator.CrossFade($"m_stand{_ctx.Facing}", 0.1f);
        }
    }
}