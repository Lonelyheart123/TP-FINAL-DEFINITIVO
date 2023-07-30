using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyStates
{
    public class OppositeLeaderDie<T> : State<T>
    {
        OppositeLeaderModel _enemy;
        public OppositeLeaderDie(OppositeLeaderModel enemyModel)
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