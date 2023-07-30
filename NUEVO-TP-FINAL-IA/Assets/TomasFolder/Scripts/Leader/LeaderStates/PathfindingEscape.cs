using UnityEngine;

namespace EnemyStates
{
    public class PathfindingEscape<T> : State<T>
    {
        ISteering _steering;
        PathfindingEnemyModel _enemy;
        PathfindingEnemyController _enemyController;
        OppositeLeaderModel oppositeLeader;
        public PathfindingEscape(PathfindingEnemyModel enemyModel, PathfindingEnemyController enemyController, ISteering flee, OppositeLeaderModel op)
        {
            _enemy = enemyModel;
            _enemyController = enemyController;
            _steering = flee;
            oppositeLeader = op;
            InitializedSteering();
        }
        void InitializedSteering()
        {
            var flee = new Flee(_enemyController.transform, oppositeLeader.transform);
            _steering = flee;
        }
        public Vector3 GetDir()
        {
            Vector3 dir = _steering.GetDir();
            return dir;
        }
        public override void Awake()
        {
            _enemy.agentController.EscapeRun();
        }
        public override void Execute()
        {
            if (_enemy._currentLife <= 40)
            {
                //_enemy.Run();
                _enemy.Move(GetDir());
                Debug.Log("ESCAPE");
            }
        }
    }
}