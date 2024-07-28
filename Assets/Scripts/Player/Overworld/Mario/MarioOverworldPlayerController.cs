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
    
    //Physics
    [SerializeField] private int _moveSpeed = 5;
    private float _velocity;
    private float _gravity;
    private float _initialJumpVelocity;
    private float _fallMultiplier = 2f;
    private float _maxJumpHeight = 4f;
    private float _maxJumpTime = 0.65f;
    private bool _isGrounded;
    private bool _isJumping;
    
    //Controls
    private PlayerInput _input;
    private InputAction _move;
    private InputAction _cycleActions;
    private InputAction _resetActions;
    private InputAction _mAction; // For all Mario actions;
    private Vector2 _cMoveVector;
    
    //Character Controller
    private CharacterController _controller;
    
    //Text
    [SerializeField] private TextMeshProUGUI _actionsText;
    
    //Sprite
    [SerializeField] private GameObject _child;
    
    //Getters and Setters
    public MarioOverworldBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public MarioOverworldBaseAction CurrentAction { get { return _currentAction; } set { _currentAction = value; } }
    public Animator MarioAnimator { get { return _animator; } set { _animator = value; } }
    public CharacterController MarioController { get { return _controller; } set { _controller = value; } }
    public string Facing { get { return _facing; } set { _facing = value; } }
    public Transform Cam { get { return _cam; } set { _cam = value; } }
    
    //Input
    public Vector2 CharacterMove { get { return _cMoveVector; } set { _cMoveVector = value; } }
    public float MoveAngle { get { return _moveAngle; } set { _moveAngle = value; } }
    public InputAction MarioAction { get { return _mAction; } }
    
    //Physics
    public int MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float Velocity { get { return _velocity; } set { _velocity = value; } }
    public float Gravity { get { return _gravity; } set { _gravity = value; } }
    public float InitialJumpVelocity { get { return _initialJumpVelocity; } }
    public float FallMultiplier { get { return _fallMultiplier; } set { _fallMultiplier = value; } }
    public float MaxJumpHeight { get { return _maxJumpHeight; } set { _maxJumpHeight = value; } }
    public float MaxJumpTime { get { return _maxJumpTime; } set { _maxJumpTime = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }

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
        _mAction = _input.actions["_mAction"];
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
        
        // Jump Setup
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(_maxJumpTime / 2, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / (_maxJumpTime / 2);
        _isJumping = false;
        
        //Setup Debug text
        _actionsText.text = $"Current Action: {PlayerOverworldActions.JUMP}";
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Current State: " + _currentState.GetType());
        _isGrounded = CheckGrounded();
        OnMove();
        CycleActions();
        _currentState.UpdateState();
    }
    
    private void OnMove()
    {
        Debug.Log("Input: " + _move.ReadValue<Vector2>());
        _cMoveVector = _move.ReadValue<Vector2>();
    }
    
    private bool CheckGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
    } 

    private void CycleActions()
    {
        if (_cycleActions.triggered)
        {
            _currentActionIndexValue ++;
            if (_currentActionIndexValue >= _actionsArray.Length)
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
