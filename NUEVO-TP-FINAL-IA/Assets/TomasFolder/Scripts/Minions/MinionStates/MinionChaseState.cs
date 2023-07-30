using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionChaseState<T> : MinionStateBase<T>

{
    Vector3 _dir;
    float predictionTime;
    OppositeLeaderModel _target;
    ISteering _pursuit;
    Transform _origin;

    public MinionChaseState(Transform transform, OppositeLeaderModel enemyLeader, ISteering steering)
    {
        _target = enemyLeader;
        _origin = transform;
    }
    public override void Awake()
    {
        base.Awake();
        InitializedSteering();
        predictionTime = 2f;
    }
    void InitializedSteering()
    {
        var pursuit = new Pursuit(_origin, _target, predictionTime);
        _pursuit = pursuit;
    }
    void MoveToPlayer()
    {
        _dir = _pursuit.GetDir();
        _minionModel.speed = 3;

        if (_minionModel.IsInSight(_target.transform))
        {
            _minionModel.LookDir(_dir);
            _minionModel.Move(_dir);
            if((Vector3.Distance(_minionModel.transform.position, _target.transform.position) <= 4f))
            {
                _minionModel.enemyInAtkRange = true;
                _minionModel.enemyDetected = false;
            }
            else
            {
                _minionModel.enemyInAtkRange = false;
            }
        }
        else
        {
            _minionModel.enemyDetected = false;
            //Debug.Log("CHASE PATH - ELSE");
        }
    }
    public override void Execute()
    {
        base.Execute();
        MoveToPlayer();
    }
}
