using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionModel : MonoBehaviour, IBoid, IPoints
{
    [Header("ISTEERING-----------")]
    public float rangeSteering;
    public float angleSteering;
    public ISteering currentSterring;
    

    [Header("LINE OF SIGHT-------")]
    public float rangeLOS;
    public float angleLOS;
    public LayerMask obstacleMaskLOS;

    [Header("FLOCKING------------")]
    public float radiusFlocking;

    [Header("STATS BASIC---------")]
    [SerializeField] float maxHP;
    public float currentHP;
    public float speed;
    public float rotSpeed;

    public List<Vector3> waypoints;
    public bool readyToMove;
    int _nextPoint = 0;

    [Header("TREE QUESTIONS-------")]
    public bool itsAlive;
    public bool itsHurt;
    public bool enemyDetected;
    public bool enemyInAtkRange;
    public bool potionDetected;
    public bool boxDetected;
    public bool boxInAtkRange;


/// ///////////

    public FlockingManager fManager;
    public Leader fLeader;
    public Avoidance fAvoidance;
    public Alignment fAlignment;
    Rigidbody _rb;
    private void Awake()
    {
        //monsterAgent = FindObjectOfType<MonstersAgent>();
        _rb = GetComponent<Rigidbody>();
        fManager = GetComponent<FlockingManager>();
        fLeader = GetComponent<Leader>();
        fAvoidance = GetComponent<Avoidance>();
        fAlignment = GetComponent<Alignment>();
    }
    public Vector3 Position => transform.position;
    public Vector3 Front => transform.forward;
    public float Radius => radiusFlocking;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Position, radiusFlocking);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rangeLOS);
        Gizmos.DrawWireSphere(transform.position, rangeLOS);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angleLOS / 2, 0) * transform.forward * rangeLOS);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angleLOS / 2, 0) * transform.forward * rangeLOS);
    }
    public void Move(Vector3 dir)
    {
        dir *= speed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }
    public void LookDir(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
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
                    //isLeaderIdle = true;
                    return;
                }
            }
            Move(dir.normalized);
        }
    }
    public bool IsInSight(Transform target)
    {
        Vector3 diff = (target.position - transform.position);
        float distance = diff.magnitude;
        if (distance > rangeLOS) return false;

        float angleToTarget = Vector3.Angle(transform.forward, diff);
        if (angleToTarget > angleLOS / 2) return false;

        Vector3 dirToTarget = diff.normalized;
        if (Physics.Raycast(transform.position, dirToTarget, distance, obstacleMaskLOS)) return false;

        return true;
    }
    public void Attack(OppositeLeaderModel target)
    {
        //target.GetDamage(10);
    }

    public void Died()
    {
        this.gameObject.SetActive(false);
    }
    public bool IsInAtkRange() => enemyInAtkRange=true;
    public bool Detected() => enemyDetected;
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, obsRadius);
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, obsAngle / 2, 0) * transform.forward * obsRadius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -obsAngle / 2, 0) * transform.forward * obsRadius);
    }
    */
}
