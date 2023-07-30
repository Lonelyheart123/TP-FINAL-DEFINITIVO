using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackState<T> : MinionStateBase<T>
{
    Vector3 _dir;
    float cdAttack;
    OppositeLeaderModel _target;
    ISteering _seek;
    Transform _origin;
    public MinionAttackState(Transform transform, OppositeLeaderModel target)
    {
        _target = target;
        _origin = transform;
    }
    
    void InitializedSteering()
    {
        var seek = new Seek(_origin, _target.transform);
        _seek = seek;
    }
    public override void Awake()
    {
        base.Awake();
        InitializedSteering();
        cdAttack = 1f;
    }
    public override void Execute()
    {
        base.Execute();
        _dir = _seek.GetDir();
        _minionModel.LookDir(_dir);
        _minionModel.speed = 0;
        
        cdAttack -= Time.deltaTime;
        if (cdAttack < 0 && _minionModel.IsInAtkRange()==true)
        {
            _minionModel.Attack(_target); cdAttack = 1f;
            Debug.Log("ATTACK!!");
        }
        else
        {
            _minionModel.enemyDetected = true;
        }
    }
}
