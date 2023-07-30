using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class OppositeLeaderEscape<T> : State<T>
    {
        ISteering _steering;
        PathfindingEnemyModel _enemy;
        OppositeLeaderController _enemyController;
        OppositeLeaderModel oppositeLeader;
        public OppositeLeaderEscape(PathfindingEnemyModel enemyModel, OppositeLeaderController enemyController, ISteering flee, OppositeLeaderModel op)
        {
            _enemy = enemyModel;
            _enemyController = enemyController;
            _steering = flee;
            oppositeLeader = op;
            InitializedSteering();
        }
        void InitializedSteering()
        {
            var flee = new Flee(_enemyController.transform, _enemy.transform);
            _steering = flee;
        }
        public Vector3 GetDir()
        {
            Vector3 dir = _steering.GetDir();
            return dir;
        }
        public override void Awake()
        {
            oppositeLeader.agentController.EscapeRun();
        }
        public override void Execute()
        {
            if (oppositeLeader._currentLife <= 40)
            {
                //_enemy.Run();
                oppositeLeader.Move(GetDir());
                Debug.Log("ESCAPE");
            }
        }
    }
}