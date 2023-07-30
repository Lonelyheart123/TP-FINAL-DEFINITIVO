using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public List<Vector3> waypoints;
    int _nextPoint = 0;
    public float rangeInSight;
    public float angleInSight;
    public LayerMask obstacleMask;
    public bool readyToMove;
    ///
    public float speed;
    public float rotSpeed = 5;
    public Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 dir)
    {
        Vector3 dirSpeed = dir * speed;
        dirSpeed.y = _rb.velocity.y;
        _rb.velocity = dirSpeed;
    }
    public void LookDir(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotSpeed);
    }

    public Vector3 GetFoward => transform.forward;

    public float GetVel => _rb.velocity.magnitude;
    private void Update()
    {
        Debug.Log(_rb);
    }
    public void Attack(Vector3 dir)
    {
        //EnemyBullet bullet = Instantiate<EnemyBullet>(_enemyBullet);
        //bullet.transform.position = transform.position;
        //bullet.SetDir = dir;
        //anim.Shoot();
    }
    public bool IsInSight(Transform target)
    {
        Vector3 diff = (target.position - transform.position);
        float distance = diff.magnitude;
        if (distance > rangeInSight) return false;

        float angleToTarget = Vector3.Angle(transform.forward, diff);
        if (angleToTarget > angleInSight / 2) return false;

        Vector3 dirToTarget = diff.normalized;
        if (Physics.Raycast(transform.position, dirToTarget, distance, obstacleMask)) return false;

        return true;
    }
    public bool IsInSight(out Transform target)
    {
        var colls = Physics.OverlapSphere(transform.position, rangeInSight/*,minionsMask*/);
        foreach (var item in colls)
        {
            target = item.transform;
            Vector3 diff = (target.position - transform.position);
            float distance = diff.magnitude;
            if (distance > rangeInSight) return false;

            float angleToTarget = Vector3.Angle(transform.forward, diff);
            if (angleToTarget > angleInSight / 2) return false;

            Vector3 dirToTarget = diff.normalized;
            if (Physics.Raycast(transform.position, dirToTarget, distance, obstacleMask)) return false;

            return true;
        }
        target = null;
        return false;
    }
    public void SetWayPoints(List<Node> newPoints)
    {
        var list = new List<Vector3>();
        for (int i = 0; i < newPoints.Count; i++)
        {
            list.Add(newPoints[i].transform.position);
        }
        SetWayPoints(list);
    }
    public void SetWayPoints(List<Vector3> newPoints)
    {
        _nextPoint = 0;
        if (newPoints.Count == 0) return;
        waypoints = newPoints;
        var pos = waypoints[_nextPoint];
        pos.y = transform.position.y;
        readyToMove = true;
    }
    public void Run()
    {
        if (waypoints.Count > 0)
        {
            var point = waypoints[_nextPoint];
            var posPoint = point;
            posPoint.y = transform.position.y;
            Vector3 dir = posPoint - transform.position;
            if (dir.magnitude < 0.2f)
            {
                if (_nextPoint + 1 < waypoints.Count)
                    _nextPoint++;
                else
                {
                    readyToMove = false;
                    return;
                }
            }
            Move(dir.normalized);
            LookDir(dir);
        }
    }
}
