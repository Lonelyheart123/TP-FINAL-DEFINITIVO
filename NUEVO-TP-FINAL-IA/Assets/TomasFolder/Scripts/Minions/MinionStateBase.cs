using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStateBase<T> : State<T>
{
    protected MinionModel _minionModel;
    protected FSM<T> _fsm;
    public void InitializedState(MinionModel model, FSM<T> fsm)
    {
        _minionModel = model;
        _fsm = fsm;
    }
}
