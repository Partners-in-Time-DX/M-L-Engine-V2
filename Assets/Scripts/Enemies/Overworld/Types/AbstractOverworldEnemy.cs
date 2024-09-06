using System;
using Enemies.Overworld.States;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Overworld.Types
{
    public abstract class AbstractOverworldEnemy : CustomBillboard
    {
        // Serializable Fields
        [field: SerializeField] protected string _enemyName;
        [field: SerializeField] protected bool _canStomp;
        [field: SerializeField] protected bool _canHammer;
        [field: SerializeField] protected float _speed = 5;
        
        // Sprite
        [field: SerializeField] protected GameObject _child;
        
        // States
        private EnemyOverworldBaseState _currentState;
        private EnemyOverworldStateFactory _stateFactory;
        
        // Transform
        private Vector3 _originalPosition;
        
        // Navmesh
        private NavMeshAgent _agent;
        
        public EnemyOverworldBaseState CurrentState { get => _currentState; set => _currentState = value; }
        public Vector3 OriginalPosition { get => _originalPosition; set => _originalPosition = value; }

        private void Start()
        {
            Init(_child); // Setup Billboard
            
            //Setup default state
            _stateFactory = new EnemyOverworldStateFactory(this);
            _currentState = _stateFactory.Idle(); // Default should be idle
            _currentState.EnterState();

            // Store original position for reset state
            _originalPosition = transform.position;
            
            // Setup NavMeshAgent
            _agent = GetComponent<NavMeshAgent>();
            
            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            _agent.autoBraking = false;
        }

        protected override void SetAnimation()
        {
            _currentState.AnimateState();
        }
    }
}