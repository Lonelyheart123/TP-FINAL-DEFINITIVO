using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoints
{
    public int dmg;
    public float speed;
    public float speedRot;
    public float timeLife;
    float _counterLife;
    Vector3 _dir;
    Rigidbody _rb;
    public bool readyToMove;
    int _nextPoint = 0;
    public List<Vector3> waypoints;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public Vector3 SetDir
    {
        set
        {
            _dir = value;
        }
    }
    public void Shoot(Vector3 dir)
    {
        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    public void Update()
    {
        ExecuteTimeLife();
        _rb.velocity = _dir * speed;
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        transform.position += Time.deltaTime * dir * speed; ;
        transform.forward = Vector3.Lerp(transform.forward, dir, speedRot * Time.deltaTime);
    }
    void ExecuteTimeLife()
    {
        _counterLife += Time.deltaTime;
        if (_counterLife > timeLife)
        {
            Destroy(this.gameObject);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        PlayerModel player = collision.gameObject.GetComponent<PlayerModel>();
    //        player.GetDamage(dmg);
    //    }
    //    Destroy(this.gameObject);
    //}
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
        transform.position = pos;
        readyToMove = true;
    }
    public void Run()
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
        Shoot(dir.normalized);
    }
}
