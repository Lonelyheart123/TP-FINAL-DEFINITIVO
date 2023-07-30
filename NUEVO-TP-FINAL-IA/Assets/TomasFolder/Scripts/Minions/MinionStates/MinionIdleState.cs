using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionIdleState<T> : MinionStateBase<T>
{
    //float tiempoIdle;
    //Vector3 dir;
    //public override void Awake()
    //{
    //    base.Awake();
    //    _zombieModel.fLeader.multiplier = 0f;
    //    _zombieModel.tiempoIdle = 3f;
    //    //_zombieModel.fAlignment.multiplier = 0.5f;
    //    _zombieModel.fPredator.multiplier = 6f;
    //}
    //public override void Execute()
    //{
    //    base.Execute();
    //    dir = _zombieModel.fManager.RunFlockingDir();
    //    _zombieModel.tiempoIdle -= Time.deltaTime;
    //    _zombieModel.Move(dir);
    //    _zombieModel.LookDir(dir);
    //    //Debug.Log("IDLE ZOMBIEE");
    //    if (_zombieModel.tiempoIdle < 0)
    //    {
    //        Sleep();
    //    }
    //}
    //public override void Sleep()
    //{
    //    _zombieModel.tiempoIdle = 3;
    //    _zombieModel.fLeader.multiplier = 0f;
    //    //_zombieModel.fAlignment.multiplier = 0.5f;
    //    _zombieModel.fPredator.multiplier = 0f;
    //    base.Sleep();
    //}
}
