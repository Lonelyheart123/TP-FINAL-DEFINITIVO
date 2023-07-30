using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionFollowState<T> : MinionStateBase<T>
{
    Vector3 _dir;
    Transform _targetLeader;
    //ITreeNode _rootTree;
    public MinionFollowState(/*ITreeNode root, */OppositeLeaderModel targetLeader)
    {
        //_rootTree = root;
        _targetLeader = targetLeader.transform;        
    }
    public override void Awake()
    {
        base.Awake();
        //_minionModel.fLeader._multiplier = 0.5f;
    }

    public override void Execute()
    {
        base.Execute();
        if (_minionModel.IsInSight(_targetLeader))
        {
            Debug.Log("CHASE Leader Start");
            _minionModel.enemyDetected = true;
            //_rootTree.Execute();
        }
        else
        {
            Debug.Log("Follow");
            _dir = _minionModel.fManager.RunFlockingDir();
            _minionModel.Move(_dir);
            _minionModel.LookDir(_dir);
        }
    }
}
