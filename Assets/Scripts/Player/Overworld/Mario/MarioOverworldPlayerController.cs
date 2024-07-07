using System;
using System.Collections.Generic;
using System.Linq;
using Player.Overworld.Enums;
using Player.Overworld.Mario.States;
using UnityEngine;

public class MarioOverworldPlayerController : MonoBehaviour
{
    //Actions
    private MarioOverworldActionFactory _actionFactory;
    private PlayerOverworldActions[] _actionsArray;
    private MarioOverworldBaseAction _currentAction;
    
    //States
    private MarioOverworldStateFactory _stateFactory;
    private MarioOverworldBaseState _currentState;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Setup default action
        _actionFactory = new MarioOverworldActionFactory(this);
        _actionsArray = _actionFactory.ActionsToArray();
        _currentAction = _actionFactory.GetCurrentAction(PlayerOverworldActions.JUMP);
        
        //Setup default state
        _stateFactory = new MarioOverworldStateFactory(this);
        _currentState = _stateFactory.Idle();
        _currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
