using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class PathfindingChase<T> : State<T>
    {
        PathfindingEnemyModel _enemy;
        PathfindingEnemyController _enemyController;
        public float predictionTime = 1;
        Transform _transform;
        public ISteering _pursuit;
        ITreeNode _root;
        Vector3 sbDir;


        public PathfindingChase(
            PathfindingEnemyModel enemyModel,
            PathfindingEnemyController enemyController,
            ITreeNode root,
            Transform Transform,
            ISteering Pursuit)
        {
            _enemy = enemyModel;
            _enemyController = enemyController;            
            _root = root;
            _transform = Transform;
            _pursuit = Pursuit;
            InitializedSteering();
            //Debug.Log("Chase Awake");
        }
        void InitializedSteering()
        {
            var pursuit = new Pursuit(_transform, _enemy, predictionTime);
            _pursuit = pursuit;
        }
        public Vector3 GetDir()
        {
            Vector3 dir = _pursuit.GetDir();
            return dir;
        }
        public void SetNewSteering(ISteering newSteering)
        {
            _pursuit = newSteering;
        }
        void MoveToPlayer()
        {
            sbDir = GetDir();
            bool isLineOfSight = _enemy.IsInSight(_enemy.transform);
            bool isInShootRange = _enemyController.ShootRange();
            _enemy.speed = 3;
            if (isInShootRange)
            {
                Debug.Log("1");
                _enemy.CanSeePlayer();
                _enemy.LookDir(sbDir);
                _root.Execute();
            }
            else if (isLineOfSight)
            {
                //Debug.Log("2");
                _enemy.CanSeePlayer();

                Vector3 dir = _enemy.transform.position - _enemy.transform.position;
                _enemy.LookDir(sbDir);
                _enemy.Move(sbDir);
            }
            else
            {
                //Debug.Log("3");
                _enemy.CantSeePlayer();
                _root.Execute();
                Debug.Log("CHASE PATH - ELSE");
            }
        }
        public override void Awake()
        {
            InitializedSteering();
        }
        public override void Execute()
        {
            MoveToPlayer();
            Debug.Log("CHASE PATH");
        }
        public override void Sleep()
        {
            //Debug.Log("Enemy ChaseState sleep");
        }
    }
}