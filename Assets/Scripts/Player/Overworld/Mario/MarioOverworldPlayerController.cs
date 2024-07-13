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
    
    //Stats
    [SerializeField] private int _moveSpeed = 5;
    
    //Controls
    private PlayerInput _input;
    private InputAction _move;
    private InputAction _cycleActions;
    private InputAction _resetActions;
    private Vector2 _cMoveVector;
    
    //Character Controller
    private CharacterController _controller;
    
    //Text
    [SerializeField] private TextMeshProUGUI _actionsText;
    
    //Sprite
    [SerializeField] private GameObject _child;
    
    //Getters and Setters
    public MarioOverworldBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Vector2 CharacterMove { get { return _cMoveVector; } set { _cMoveVector = value; } }
    public float MoveAngle { get { return _moveAngle; } set { _moveAngle = value; } }
    public Animator MarioAnimator { get { return _animator; } set { _animator = value; } }
    public CharacterController MarioController { get { return _controller; } set { _controller = value; } }
    public string Facing { get { return _facing; } set { _facing = value; } }
    public Transform Cam { get { return _cam; } set { _cam = value; } }
    public int MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Initialise Billboard
        Init(_child);
        
        //Animator
        _animator = _child.GetComponent<Animator>();
        
        //Character Controller
        _controller = GetComponent<CharacterController>();
        
        //Setup Controller
        _input = GameObject.FindWithTag("ControllerManager").GetComponent<PlayerInput>();
        _input.SwitchCurrentActionMap("Player");
        _move = _input.actions["Move"];
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
    private void Update()
    {
        Debug.Log("Current State: " + _currentState.GetType());
        OnMove();
        CycleActions();
        _currentState.UpdateState();
    }
    
    private void OnMove()
    {
        Debug.Log("Input: " + _move.ReadValue<Vector2>());
        _cMoveVector = _move.ReadValue<Vector2>();
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
