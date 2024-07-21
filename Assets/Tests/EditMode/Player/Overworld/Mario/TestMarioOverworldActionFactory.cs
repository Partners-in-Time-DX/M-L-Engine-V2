using System.Collections;
using ErrorHandling.Exceptions;
using NUnit.Framework;
using Player.Overworld.Enums;
using Player.Overworld.Mario.States;
using Tests.EditMode.ObjectFactory;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMarioOverworldActionFactory
{
    [Test]
    public void TestMarioOverworldFactoryReturnJump()
    {
        //Generate monobehaviour using TestableObjectFactory
        var playerController = TestableObjectFactory.Create<MarioOverworldPlayerController>();

        var actionFactory = new MarioOverworldActionFactory(playerController);

        var currentAction = actionFactory.GetCurrentAction(PlayerOverworldActions.JUMP);

        Assert.AreEqual(currentAction.GetType(), typeof(MarioOverworldJumpAction));
    }
    
    [Test]
    public void TestMarioOverworldFactoryReturnHammer()
    {
        //Generate monobehaviour using TestableObjectFactory
        var playerController = TestableObjectFactory.Create<MarioOverworldPlayerController>();

        var actionFactory = new MarioOverworldActionFactory(playerController);

        var currentAction = actionFactory.GetCurrentAction(PlayerOverworldActions.HAMMER);

        Assert.AreEqual(currentAction.GetType(), typeof(MarioOverworldHammerAction));
    }
    
    [Test]
    public void TestMarioOverworldFactoryThrowsInvalidPlayerActionException()
    {
        // Cast outside of range to produce invalid enum
        var invalidActionEnum = (PlayerOverworldActions)(-1); 
        
        //Generate monobehaviour using TestableObjectFactory
        var playerController = TestableObjectFactory.Create<MarioOverworldPlayerController>();

        var actionFactory = new MarioOverworldActionFactory(playerController);

        Assert.Throws<InvalidPlayerActionException>(() =>
        {
            actionFactory.GetCurrentAction(invalidActionEnum);
        });
    }
}
