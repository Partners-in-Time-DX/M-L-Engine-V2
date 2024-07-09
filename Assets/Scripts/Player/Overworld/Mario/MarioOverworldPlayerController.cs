using System;
using System.Collections.Generic;
using System.Linq;
using Player.Overworld.Enums;
using Player.Overworld.Mario.States;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarioOverworldPlayerController : CustomBillboard
{
    //Actions
    private MarioOverworldActionFactory _actionFactory;
    private PlayerOverworldActions[] _actionsArray;
    private MarioOverworldBaseAction _currentAction;
    private int _currentActionIndexValue = 0;
    
    //States
    private MarioOverworldStateFactory _stateFactory;
    private MarioOverworldBaseState _currentState;
    
    //Controls
    private PlayerInput _input;
    private InputAction _cycleActions;
    private InputAction _resetActions;
    
    //Text
    [SerializeField] private TextMeshProUGUI _actionsText;
    
    //Sprite
    [SerializeField] private GameObject _child;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialise Billboard
        base.Init(_child);
        
        //Setup Controller
        _input = GameObject.FindWithTag("ControllerManager").GetComponent<PlayerInput>();
        _input.SwitchCurrentActionMap("Player");
        _cycleActions = _input.actions["_cycleActions"];
        _resetActions = _input.actions["_resetActions"];
        
        //Setup default action
        _actionFactory = new MarioOverworldActionFactory(this);
        _actionsArray = _actionFactory.ActionsToArray(); // Gets list of actions
        _currentAction = _actionFactory.GetCurrentAction(PlayerOverworldActions.JUMP); // Default should be jump
        
        //Setup default state
        _stateFactory = new MarioOverworldStateFactory(this);
        _currentState = _stateFactory.Idle(); // Default should be idle
        _currentState.EnterState();
        
        //Setup Debug text
        _actionsText.text = $"Current Action: {PlayerOverworldActions.JUMP}";
    }

    // Update is called once per frame
    void Update()
    {
        CycleActions();
        _currentState.UpdateState();
    }

    private void CycleActions()
    {
        if (_cycleActions.triggered)
        {
            _currentActionIndexValue += 1;
            if (_currentActionIndexValue == _actionsArray.Length)
            {
                _currentActionIndexValue = 0;
            }
            
            _currentAction = _actionFactory.GetCurrentAction(_actionsArray[_currentActionIndexValue]);
            _actionsText.text = $"Current Action: {_actionsArray[_currentActionIndexValue].ToString()}";
        }
        else if (_resetActions.triggered)
        {
            _currentActionIndexValue = 0;
            
            _currentAction = _actionFactory.GetCurrentAction(_actionsArray[_currentActionIndexValue]);
            _actionsText.text = $"Current Action: {_actionsArray[_currentActionIndexValue].ToString()}";
        }
    }

    protected override void SetAnimation()
    {
        _currentState.AnimateState();
    }
}
