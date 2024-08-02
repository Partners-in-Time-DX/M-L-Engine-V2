namespace Player.Overworld.Mario.States
{
    public abstract class MarioOverworldBaseAction
    {
        protected MarioOverworldPlayerController _ctx;
        protected MarioOverworldActionFactory _factory;

        public MarioOverworldBaseAction(
            MarioOverworldPlayerController ctx, 
            MarioOverworldActionFactory factory)
        {
            _ctx = ctx;
            _factory = factory;
        }
        public abstract void HandleAction();
    }
}