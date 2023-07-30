using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDiedState<T> : MinionStateBase<T>
{
    
    public MinionDiedState()
    {
        
    }
    public override void Execute()
    {
        base.Execute();
        _minionModel.Died();
    }
}
