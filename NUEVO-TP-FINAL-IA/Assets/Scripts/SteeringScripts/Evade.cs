using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : Pursuit
{
    public Evade(Transform origin, EntityBase target, float time): base(origin, target, time)
    {
    }
    public override Vector3 GetDir()
    {
        return -base.GetDir();
    }
}
