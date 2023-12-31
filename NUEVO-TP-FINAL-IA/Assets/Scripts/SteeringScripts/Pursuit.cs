using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : ISteering
{
    Transform _origin;
    EntityBase _target;
    float _time;
    public Pursuit(Transform origin, EntityBase target, float time)
    {
        _origin = origin;
        _target = target;
        _time = time;
    }
    public virtual Vector3 GetDir()
    {
        float distance = Vector3.Distance(_origin.position, _target.transform.position);

        Vector3 point = _target.transform.position + _target.GetFoward * Mathf.Clamp(_target.GetVel * _time, 0, distance);
        return (point - _origin.position).normalized;
    }
}
