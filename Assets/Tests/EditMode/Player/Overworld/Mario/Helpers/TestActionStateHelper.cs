using NUnit.Framework;
using Player.Overworld.Enums;
using Player.Overworld.Mario.Helpers;
using Player.Overworld.Mario.States;
using Player.Overworld.Mario.States.ActionStates.Jump;
using Tests.EditMode.ObjectFactory;

namespace Tests.EditMode.Player.Overworld.Mario.Helpers
{
    public class TestActionStateHelper
    {
        [Test]
        public void TestGetActionStateReturnsJump()
        {
            //Generate monobehaviour using TestableObjectFactory
            var playerController = TestableObjectFactory.Create<MarioOverworldPlayerController>();
            var stateFactory = new MarioOverworldStateFactory(playerController);
            var actionFactory = new MarioOverworldActionFactory(playerController);
            var actionStateHelper = new ActionStateHelper(stateFactory);
            var currentAction = actionFactory.GetCurrentAction(PlayerOverworldActions.JUMP);

            var currentActionState = actionStateHelper.GetActionState(currentAction);
            
            Assert.AreEqual(currentActionState.GetType(), typeof(MarioOverworldJumpState));
        }
    }
}