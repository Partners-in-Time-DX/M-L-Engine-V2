using System.Collections;
using NUnit.Framework;
using Player.Overworld.Mario.States;
using Tests.EditMode.ObjectFactory;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMarioOverworldStateFactory
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestMarioOverworldStateFactoryReturnIdle()
    {
        //Generate monobehaviour using TestableObjectFactory
        var playerController = TestableObjectFactory.Create<MarioOverworldPlayerController>();

        var stateFactory = new MarioOverworldStateFactory(playerController);

        var currentState = stateFactory.Idle();
        
        Assert.AreEqual(currentState.GetType(), typeof(MarioOverworldIdleState));
    }
    
    [Test]
    public void TestMarioOverworldStateFactoryReturnWalking()
    {
        //Generate monobehaviour using TestableObjectFactory
        var playerController = TestableObjectFactory.Create<MarioOverworldPlayerController>();

        var stateFactory = new MarioOverworldStateFactory(playerController);

        var currentState = stateFactory.Walking();
        
        Assert.AreEqual(currentState.GetType(), typeof(MarioOverworldWalkingState));
    }
}
