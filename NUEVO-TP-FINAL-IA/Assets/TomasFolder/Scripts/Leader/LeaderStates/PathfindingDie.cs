using Unity.VisualScripting;
using UnityEngine;

namespace EnemyStates
{
    public class PathfindingDie<T> : State<T>
    {
        PathfindingEnemyModel _enemy;
        public PathfindingDie(PathfindingEnemyModel enemyModel)
        {
            _enemy = enemyModel;
        }
        public override void Awake()
        {
            _enemy.Dead();
        }
        public override void Execute()
        {
            Debug.Log("Está muerto");
        }
    }
}