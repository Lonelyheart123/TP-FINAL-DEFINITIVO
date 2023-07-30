using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace EnemyStates
{
    public class PathfindingPatrol<T> : State<T>
    {
        Transform _target;
        PathfindingEnemyModel _enemy;
        private ITreeNode _root;
        public ISteering _currentSteering;

        public PathfindingPatrol(PathfindingEnemyModel EnemyModel, Transform Target, ITreeNode Root, ISteering CurrentSteering)
        {
            _target = Target;
            _enemy = EnemyModel;
            _root = Root;
            _currentSteering = CurrentSteering;
            _enemy._walkcounter = 0;
        }

        public override void Awake()
        {
            _enemy.agentController.AStarRun();
            _enemy.readyToMove = true;
        }
        public override void Execute()
        {

            if (_enemy.IsInSight(_target)) //TRANSICION CHASE
            {
                _enemy.CanSeePlayer();
                //Debug.Log("Is in sight");
                _root.Execute();
            }
            else //PATROL
            {
                Debug.Log("Patrolling");
                _enemy.CantSeePlayer();
                if (_enemy.readyToMove == true)
                {
                    _enemy.Run();
                }
                //Debug.Log("Estoy en Pathfinding Patrol");                    
                _enemy._walkcounter += Time.deltaTime;
            }
        }

        public override void Sleep()
        {
            //Debug.Log("Estoy en sleep");
        }
    }
}
