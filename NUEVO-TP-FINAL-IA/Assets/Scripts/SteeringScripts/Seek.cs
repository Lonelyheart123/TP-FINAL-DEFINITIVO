using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : ISteering
{
    Transform _target;
    Transform _origin;
    public Seek(Transform origin, Transform target)
    {
        _origin = origin;
        _target = target;
    }
    public virtual Vector3 GetDir()
    {
        //b-a
        //a= origin
        //b= target
        return (_target.position - _origin.position).normalized;
    }
}
