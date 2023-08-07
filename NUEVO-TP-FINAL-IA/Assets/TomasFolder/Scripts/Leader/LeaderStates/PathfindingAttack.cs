using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class PathfindingAttack<T> : State<T>
    {
        PathfindingEnemyModel _enemy;
        PathfindingEnemyController _enemyController;
        float time = 0.5f;
        float _counter = 0;
        Vector3 _dir;
        public EnemyBullet bulletprefab;
        ITreeNode _root;

        public PathfindingAttack(PathfindingEnemyModel enemyModel, PathfindingEnemyController enemyController, Vector3 dir, ITreeNode root)
        {
            _enemy = enemyModel;
            _enemyController = enemyController;
            _dir = dir;
            _root = root;
        }
        public override void Awake()
        {
            print("ASDSADSASDSASASAASAS");
            _enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        void StateUpdate()
        {
            _counter += Time.deltaTime;
            if (_counter >= time && _enemyController.ShootRange() == true)
            {
                _enemy._currentLife -= _enemy.damage;
                //_dir = (_target.transform.position - _enemy.transform.position).normalized;
                //_enemy.Attack(_dir);
                _counter = 0;
                if (_enemy._currentLife <= 40)
                {
                    Debug.Log("Estoy herido");
                    _enemy.isWounded = true;
                    _root.Execute();
                }
                //if (_enemy._currentLife <= 0)
                //{
                //    Debug.Log("Mori");
                //    _enemy.isAlive = false;
                //    _root.Execute();
                //}
            }
            else
            {
                _root.Execute();
            }
        }
        public override void Execute()
        {
            StateUpdate();
            //Debug.Log("Attack");
        }
        public override void Sleep()
        {
            //Debug.Log("Enemy AttackState sleep");
        }
    }
}

