using System.Collections.Generic;
using Enemies.Overworld.Types;

namespace Enemies.Overworld.States
{
    public enum EnemyOverworldStates
    {
        IDLE,
        PATROL,
        ALERT,
        RESET,
        DEFEAT
    }
    
    public class EnemyOverworldStateFactory
    {
        private AbstractOverworldEnemy _ctx;
        private Dictionary<EnemyOverworldStates, EnemyOverworldBaseState> _states =
            new Dictionary<EnemyOverworldStates, EnemyOverworldBaseState>();
        
        public EnemyOverworldStateFactory(AbstractOverworldEnemy ctx)
        {
            _ctx = ctx;

            _states[EnemyOverworldStates.IDLE] = new EnemyOverworldIdleState(_ctx, this);
            _states[EnemyOverworldStates.PATROL] = new EnemyOverworldPatrolState(_ctx, this);
            _states[EnemyOverworldStates.ALERT] = new EnemyOverworldAlertState(_ctx, this);
            _states[EnemyOverworldStates.RESET] = new EnemyOverworldResetState(_ctx, this);
        }

        public EnemyOverworldBaseState Idle()
        {
            return _states[EnemyOverworldStates.IDLE];
        }
        
        public EnemyOverworldBaseState Patrol()
        {
            return _states[EnemyOverworldStates.PATROL];
        }
        
        public EnemyOverworldBaseState Alert()
        {
            return _states[EnemyOverworldStates.ALERT];
        }
        
        public EnemyOverworldBaseState Reset()
        {
            return _states[EnemyOverworldStates.RESET];
        }
    }
}