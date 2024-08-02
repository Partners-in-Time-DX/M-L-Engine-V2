using Player.Overworld.Mario.States;
using UnityEngine;

namespace Player.Overworld.Mario.Actions
{
    public class MarioOverworldJumpAction : MarioOverworldBaseAction
    {
        public MarioOverworldJumpAction(
            MarioOverworldPlayerController ctx, 
            MarioOverworldActionFactory factory) : base(ctx, factory)
        {
        }
        
        public override void HandleAction()
        {
            _ctx.Velocity += _ctx.Gravity * Time.deltaTime;
            _ctx.MarioController.Move(new Vector3(0f, _ctx.Velocity * Time.deltaTime));
        }
    }
}