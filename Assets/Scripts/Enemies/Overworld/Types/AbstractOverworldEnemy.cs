using System;
using Enemies.Overworld.States;
using UnityEngine;

namespace Enemies.Overworld.Types
{
    public class AbstractOverworldEnemy : CustomBillboard
    {
        // States
        private EnemyOverworldBaseState _currentState;
        private EnemyOverworldStateFactory _stateFactory;
        
        // Transform
        private Vector3 _originalPosition;
        
        public string name;
        public bool canStomp;
        public bool canHammer;
        public float speed = 5;
        
        public EnemyOverworldBaseState CurrentState { get => _currentState; set => _currentState = value; }

        private void Start()
        {
            //Setup default state
            _stateFactory = new EnemyOverworldStateFactory(this);
            _currentState = _stateFactory.Idle(); // Default should be idle
            _currentState.EnterState();

            // Store original position for reset state
            _originalPosition = transform.position;
        }

        protected override void SetAnimation()
        {
            _currentState.AnimateState();
        }
    }
}